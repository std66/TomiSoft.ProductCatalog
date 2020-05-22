using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
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

        public Task<EmptyResultBM<DeleteProductExplanation>> DeleteProductAsync(string barcode) {
            return productDataManager.DeleteProductAsync(barcode);
        }

        public Task<LocalizedProductBM> GetProductAsync(string barcode, string languageCode) {
            return productDataManager.GetLocalizedProductAsync(barcode, languageCode);
        }

        public async Task<GetProductsByCategoryResultBM> GetProductsByCategoryAsync(int categoryId, string languageCode) {
            return new GetProductsByCategoryResultBM(
                products: await productDataManager.GetLocalizedProductByCategoryAsync(categoryId, languageCode),
                category: await categoryDataManager.GetAsync(categoryId, languageCode)
            );
        }
    }
}
