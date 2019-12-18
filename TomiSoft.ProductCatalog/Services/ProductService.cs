using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Services {
    internal class ProductService : IProductService {
        private readonly IProductDataManager productDataManager;
        private readonly ICategoryDataManager categoryDataManager;

        public ProductService(IProductDataManager productDataManager, ICategoryDataManager categoryDataManager) {
            this.productDataManager = productDataManager;
            this.categoryDataManager = categoryDataManager;
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
