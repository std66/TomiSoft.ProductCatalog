using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Services {
    internal class ProductService {
        private readonly IManufacturerDataManager manufacturerDataManager;
        private readonly ICategoryDataManager categoryDataManager;

        public ProductService(IManufacturerDataManager manufacturerDataManager, ICategoryDataManager categoryDataManager) {
            this.manufacturerDataManager = manufacturerDataManager;
            this.categoryDataManager = categoryDataManager;
        }

        public Task<LocalizedProductBM> GetProduct(string barcode, string languageCode) {
            return Task.FromResult(new LocalizedProductBM(
                barcode: barcode,
                languageCode: languageCode,
                localizedName: null,
                category: null,
                manufacturer: null
            ));
        }
    }
}
