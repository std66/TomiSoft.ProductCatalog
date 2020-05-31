using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;

namespace TomiSoft.ProductCatalog.Services {
    public interface IManufacturerService {
        Task<ResultBM<ManufacturerBM, AddManufacturerExplanation>> AddManufacturerAsync(string name, ManufacturerLocationBM location, Uri website = null, ManufacturerLogoBM logo = null);
        Task DeleteAsync(ManufacturerIdBM manufacturerId);
        Task<IReadOnlyList<ManufacturerBM>> GetAllManufacturersAsync();
        Task<IReadOnlyList<BriefManufacturerBM>> GetAllManufacturersBriefAsync();
        Task<ManufacturerBM> GetManufacturerAsync(ManufacturerIdBM manufacturerId);
        Task UpdateLogoAsync(ManufacturerIdBM manufacturerId, ManufacturerLogoBM logo);
        Task UpdateWebsiteAsync(ManufacturerIdBM manufacturerId, Uri websiteUri);
        Task<ResultBM<ManufacturerLogoBM, GetManufacturerLogoExplanation>> GetLogoAsync(ManufacturerIdBM manufacturerId);
    }
}