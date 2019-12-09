using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;

namespace TomiSoft.ProductCatalog.DataManagement {
    public interface ICategoryDataManager {
        Task<IEnumerable<LocalizedCategoryBM>> GetAllAsync(string languageCode);
        Task<LocalizedCategoryBM> GetAsync(int id, string languageCode);
    }
}
