using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;

namespace TomiSoft.ProductCatalog.DataManagement {
    public interface IManufacturerDataManager {
        Task<ManufacturerBM> GetAsync(ManufacturerIdBM id);
        Task<ResultBM<ManufacturerLogoBM, GetManufacturerLogoExplanation>> GetLogoAsync(ManufacturerIdBM id);
        Task<ResultBM<ManufacturerBM, AddManufacturerExplanation>> InsertAsync(ManufacturerBM manufacturer);
        Task DeleteAsync(ManufacturerBM manufacturer);
        Task<IReadOnlyList<ManufacturerBM>> GetAllAsync();
        Task<IReadOnlyList<BriefManufacturerBM>> GetAllBriefAsync();
        Task UpdateAsync(ManufacturerBM manufacturer);
    }
}
