using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;

namespace TomiSoft.ProductCatalog.DataManagement {
    public interface IManufacturerDataManager {
        Task<ManufacturerBM> GetAsync(int id);
        Task<ManufacturerLogoBM> GetLogoAsync(int id);
        Task<ResultBM<ManufacturerBM, AddManufacturerExplanation>> InsertAsync(ManufacturerBM manufacturer);
        Task DeleteAsync(ManufacturerBM manufacturer);
        Task<IReadOnlyList<ManufacturerBM>> GetAllAsync();
        Task<IReadOnlyList<BriefManufacturerBM>> GetAllBriefAsync();
        Task UpdateAsync(ManufacturerBM manufacturer);
    }
}
