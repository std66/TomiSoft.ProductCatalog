using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;

namespace TomiSoft.ProductCatalog.Services {
    public interface IManufacturerService {
        Task<ResultBM<ManufacturerBM, AddManufacturerExplanation>> AddManufacturerAsync(string name, ManufacturerLocationBM location, Uri website = null, ManufacturerLogoBM logo = null);
        Task DeleteAsync(int manufacturerId);
        Task<IReadOnlyList<ManufacturerBM>> GetAllManufacturersAsync();
        Task<IReadOnlyList<BriefManufacturerBM>> GetAllManufacturersBriefAsync();
        Task<ManufacturerLogoBM> GetLogoAsync(int manufacturerId);
        Task<ManufacturerBM> GetManufacturerAsync(int manufacturerId);
        Task UpdateLogoAsync(int manufacturerId, ManufacturerLogoBM logo);
        Task UpdateWebsiteAsync(int manufacturerId, Uri websiteUri);
    }
}