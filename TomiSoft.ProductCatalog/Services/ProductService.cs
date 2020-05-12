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

        public async Task<ResultBM<EmptyBM, CreateProductExplanation>> CreateProduct(CreateProductRequestBM createProductRequest) {
            if (await productDataManager.ProductExistsWithBarcode(createProductRequest.Barcode)) {
                return new FailureResultBM<EmptyBM, CreateProductExplanation>(CreateProductExplanation.ProductWithBarcodeAlreadyExists);
            }

            if (!await productDataManager.CreateProduct(createProductRequest)) {
                return new FailureResultBM<EmptyBM, CreateProductExplanation>(CreateProductExplanation.DatabaseError);
            }

            return new SuccessfulResultBM<EmptyBM, CreateProductExplanation>(EmptyBM.Instance);
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
