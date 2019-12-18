using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;

namespace TomiSoft.ProductCatalog.Services {
    public interface IProductService {
        Task<LocalizedProductBM> GetProductAsync(string barcode, string languageCode);
        Task<GetProductsByCategoryResultBM> GetProductsByCategoryAsync(int categoryId, string languageCode);
    }
}