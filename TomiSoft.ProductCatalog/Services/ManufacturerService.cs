using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Services {
    public class ManufacturerService : IManufacturerService {
        private readonly IManufacturerDataManager dataManager;

        public ManufacturerService(IManufacturerDataManager dataManager) {
            this.dataManager = dataManager;
        }

        public async Task<ManufacturerBM> AddManufacturerAsync(string name, ManufacturerLocationBM location, Uri website = null, ManufacturerLogoBM logo = null) {
            ManufacturerBM manufacturer = new ManufacturerBM(
                manufacturerId: default,
                name: name,
                location: location,
                websiteUri: website,
                logo: logo
            );

            return await dataManager.InsertAsync(manufacturer);
        }

        public Task<ManufacturerBM> GetManufacturerAsync(int manufacturerId) {
            return dataManager.GetAsync(manufacturerId);
        }

        public Task<IReadOnlyList<ManufacturerBM>> GetAllManufacturersAsync() {
            return dataManager.GetAllAsync();
        }

        public async Task UpdateLogoAsync(int manufacturerId, ManufacturerLogoBM logo) {
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

        public async Task UpdateWebsiteAsync(int manufacturerId, Uri websiteUri) {
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

        public Task<ManufacturerLogoBM> GetLogoAsync(int manufacturerId) {
            return dataManager.GetLogoAsync(manufacturerId);
        }

        public async Task DeleteAsync(int manufacturerId) {
            await dataManager.DeleteAsync(
                await dataManager.GetAsync(manufacturerId)
            );
        }
    }
}
