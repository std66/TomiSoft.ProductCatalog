using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;

namespace TomiSoft.ProductCatalog.DataManagement {
    public interface ICategoryDataManager {
        Task<IEnumerable<LocalizedCategoryBM>> GetAllAsync(string languageCode);
        Task<IReadOnlyList<LocalizedCategoryWithProductCountBM>> GetAllCategoriesWithProductCountAsync(string languageCode);
        Task<LocalizedCategoryBM> GetAsync(CategoryIdBM id, string languageCode);
    }
}
