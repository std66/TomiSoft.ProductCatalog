using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;

namespace TomiSoft.ProductCatalog.Services {
    public interface ICategoryService {
        Task<CategoryTreeRootBM> GetCategoryTreeAsync(string languageCode);
        Task<IReadOnlyList<LocalizedCategoryWithProductCountBM>> GetAllCategoriesAsync(string languageCode);
    }
}