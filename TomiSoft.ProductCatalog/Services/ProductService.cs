using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;
using TomiSoft.ProductCatalog.BusinessModels.Request;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Services {
    internal class ProductService : IProductService {
        private readonly IProductDataManager productDataManager;
        private readonly ICategoryDataManager categoryDataManager;

        public ProductService(IProductDataManager productDataManager, ICategoryDataManager categoryDataManager) {
            this.productDataManager = productDataManager;
            this.categoryDataManager = categoryDataManager;
        }

        public async Task<EmptyResultBM<CreateProductExplanation>> CreateProductAsync(CreateProductRequestBM createProductRequest) {
            if (await productDataManager.ProductExistsWithBarcodeAsync(createProductRequest.Barcode)) {
                return new EmptyResultBM<CreateProductExplanation>(CreateProductExplanation.ProductWithBarcodeAlreadyExists);
            }

            if (!await productDataManager.CreateProductAsync(createProductRequest)) {
                return new EmptyResultBM<CreateProductExplanation>(CreateProductExplanation.DatabaseError);
            }

            return new EmptyResultBM<CreateProductExplanation>();
        }

        public Task<EmptyResultBM<DeleteProductExplanation>> DeleteProductAsync(BarcodeBM barcode) {
            return productDataManager.DeleteProductAsync(barcode);
        }

        public Task<LocalizedProductBM> GetProductAsync(BarcodeBM barcode, string languageCode) {
            return productDataManager.GetLocalizedProductAsync(barcode, languageCode);
        }

        public async Task<GetProductsByCategoryResultBM> GetProductsByCategoryAsync(CategoryIdBM categoryId, string languageCode) {
            return new GetProductsByCategoryResultBM(
                products: await productDataManager.GetLocalizedProductByCategoryAsync(categoryId, languageCode),
                category: await categoryDataManager.GetAsync(categoryId, languageCode)
            );
        }

        public Task<ResultBM<IReadOnlyList<BarcodeBM>, GetProductBarcodesByCategoryExplanation>> GetProductBarcodesByCategoryAsync(CategoryIdBM categoryId) {
            return productDataManager.GetProductBarcodesByCategoryAsync(categoryId);
        }


        public async Task<EmptyResultBM<UpdateProductExplanation>> UpdateProductAsync(UpdateProductRequestBM updateRequest) {
            ProductBM product = await productDataManager.GetProductAsync(updateRequest.Barcode);
            if (product == null)
                return new EmptyResultBM<UpdateProductExplanation>(UpdateProductExplanation.ProductNotFound);

            //apply product name changes
            string[] productNamesToRemove = updateRequest.ProductName.Where(x => string.IsNullOrWhiteSpace(x.Value)).Select(x => x.Key).ToArray();

            //update or keep already specified languages, by ignoring those ones that need to be removed
            Dictionary<string, string> newProductNames = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> productNameEntry in product.ProductName.Where(x => !productNamesToRemove.Contains(x.Key))) {
                if (updateRequest.ProductName.ContainsKey(productNameEntry.Key)) {
                    newProductNames.Add(productNameEntry.Key, updateRequest.ProductName[productNameEntry.Key]);
                }
                else {
                    newProductNames.Add(productNameEntry.Key, product.ProductName[productNameEntry.Key]);
                }
            }

            //add new languages
            foreach (var item in updateRequest.ProductName.Where(x => !productNamesToRemove.Contains(x.Key) && !newProductNames.Keys.Contains(x.Key))) {
                newProductNames.Add(item.Key, item.Value);
            }

            //perform update
            ProductBM updatedProduct = new ProductBM(
                barcode: updateRequest.Barcode,
                manufacturerId: updateRequest.NewManufacturerId,
                categoryId: updateRequest.NewCategoryId,
                productName: newProductNames
            );

            if (!await productDataManager.UpdateProductAsync(updatedProduct))
                return new EmptyResultBM<UpdateProductExplanation>(UpdateProductExplanation.DatabaseError);

            return new EmptyResultBM<UpdateProductExplanation>();
        }
    }
}
