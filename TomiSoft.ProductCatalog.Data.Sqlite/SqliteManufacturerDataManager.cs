using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Data.Sqlite {
    internal class SqliteManufacturerDataManager : IManufacturerDataManager {
        private readonly SqliteProductCatalogDbContext dbContext;
        private readonly IMapper mapper;

        public SqliteManufacturerDataManager(SqliteProductCatalogDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task DeleteAsync(ManufacturerBM manufacturer) {
            dbContext.Manufacturers.Remove(
                await dbContext.Manufacturers.SingleAsync(x => x.Id == manufacturer.ManufacturerId)
            );

            await dbContext.SaveChangesAsync();
        }

        public Task<IReadOnlyList<ManufacturerBM>> GetAllAsync() {
            List<ManufacturerBM> result = new List<ManufacturerBM>();
            foreach (EManufacturer item in dbContext.Manufacturers) {
                result.Add(
                    mapper.Map<EManufacturer, ManufacturerBM>(item)
                );
            }

            return Task.FromResult<IReadOnlyList<ManufacturerBM>>(result);
        }

        public async Task<ManufacturerBM> GetAsync(int id) {
            return mapper.Map<EManufacturer, ManufacturerBM>(
                await dbContext.Manufacturers.SingleAsync(x => x.Id == id)
            );
        }

        public async Task<ManufacturerLogoBM> GetLogoAsync(int id) {
            var data = await dbContext.Manufacturers.Select(x => new {
                Id = x.Id,
                Data = x.LogoData,
                MimeType = x.LogoMimeType
            }).SingleAsync(x => x.Id == id);

            return new ManufacturerLogoBM(data.Data, data.MimeType);
        }

        public async Task<ManufacturerBM> InsertAsync(ManufacturerBM manufacturer) {
            EManufacturer entity = mapper.Map<ManufacturerBM, EManufacturer>(manufacturer);

            await dbContext.Manufacturers.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return mapper.Map<EManufacturer, ManufacturerBM>(entity);
        }

        public async Task UpdateAsync(ManufacturerBM manufacturer) {
            EManufacturer entity = await dbContext.Manufacturers.SingleAsync(x => x.Id == manufacturer.ManufacturerId);
            mapper.Map(manufacturer, entity);

            await dbContext.SaveChangesAsync();
        }
    }
}
