using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;

namespace TomiSoft.ProductCatalog.DataManagement {
    public interface IProductDataManager {
        Task<LocalizedProductBM> GetLocalizedProductAsync(string barcode, string languageCode);
        Task<IReadOnlyList<LocalizedProductByCategoryBM>> GetLocalizedProductByCategoryAsync(int categoryId, string languageCode);
        Task<IReadOnlyDictionary<int, int>> GetNumberOfProductsInCategoriesAsync(params int[] categoryIds);
    }
}
