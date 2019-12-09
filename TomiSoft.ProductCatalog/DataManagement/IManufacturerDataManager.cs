using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;

namespace TomiSoft.ProductCatalog.DataManagement {
    public interface IManufacturerDataManager {
        Task<ManufacturerBM> GetAsync(int id);
        Task<ManufacturerLogoBM> GetLogoAsync(int id);
        Task<ManufacturerBM> InsertAsync(ManufacturerBM manufacturer);
        Task DeleteAsync(ManufacturerBM manufacturer);
        Task<IReadOnlyList<ManufacturerBM>> GetAllAsync();
        Task UpdateAsync(ManufacturerBM manufacturer);
    }
}
