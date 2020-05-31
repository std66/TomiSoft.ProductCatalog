using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Services {
    public class ManufacturerService : IManufacturerService {
        private readonly IManufacturerDataManager dataManager;

        public ManufacturerService(IManufacturerDataManager dataManager) {
            this.dataManager = dataManager;
        }

        public Task<ResultBM<ManufacturerBM, AddManufacturerExplanation>> AddManufacturerAsync(string name, ManufacturerLocationBM location, Uri website = null, ManufacturerLogoBM logo = null) {
            ManufacturerBM manufacturer = new ManufacturerBM(
                manufacturerId: default,
                name: name,
                location: location,
                websiteUri: website,
                logo: logo
            );

            return dataManager.InsertAsync(manufacturer);
        }

        public Task<ManufacturerBM> GetManufacturerAsync(ManufacturerIdBM manufacturerId) {
            return dataManager.GetAsync(manufacturerId);
        }

        public Task<IReadOnlyList<ManufacturerBM>> GetAllManufacturersAsync() {
            return dataManager.GetAllAsync();
        }

        public Task<IReadOnlyList<BriefManufacturerBM>> GetAllManufacturersBriefAsync() {
            return dataManager.GetAllBriefAsync();
        }

        public async Task UpdateLogoAsync(ManufacturerIdBM manufacturerId, ManufacturerLogoBM logo) {
            ManufacturerBM model = await dataManager.GetAsync(manufacturerId);

            await dataManager.UpdateAsync(
                new ManufacturerBM(
                    manufacturerId: model.ManufacturerId,
                    name: model.Name,
                    location: model.Location,
                    websiteUri: model.WebsiteUri,
                    logo: logo
                )
            );
        }

        public async Task UpdateWebsiteAsync(ManufacturerIdBM manufacturerId, Uri websiteUri) {
            ManufacturerBM model = await dataManager.GetAsync(manufacturerId);

            await dataManager.UpdateAsync(
                new ManufacturerBM(
                    manufacturerId: model.ManufacturerId,
                    name: model.Name,
                    location: model.Location,
                    websiteUri: websiteUri,
                    logo: model.Logo
                )
            );
        }

        public Task<ResultBM<ManufacturerLogoBM, GetManufacturerLogoExplanation>> GetLogoAsync(ManufacturerIdBM manufacturerId) {
            return dataManager.GetLogoAsync(manufacturerId);
        }

        public async Task DeleteAsync(ManufacturerIdBM manufacturerId) {
            await dataManager.DeleteAsync(
                await dataManager.GetAsync(manufacturerId)
            );
        }
    }
}
