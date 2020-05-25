using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities.Projections;
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

        public async Task<IReadOnlyList<ManufacturerBM>> GetAllAsync() {
            return await dbContext.Manufacturers
                .ProjectTo<ManufacturerBM>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<BriefManufacturerBM>> GetAllBriefAsync() {
            List<PBriefManufacturer> list = await dbContext.Manufacturers
                .ProjectTo<PBriefManufacturer>(mapper.ConfigurationProvider)
                .ToListAsync();

            return list
                .Select(x => mapper.Map<PBriefManufacturer, BriefManufacturerBM>(x))
                .ToList();
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

        public async Task<ResultBM<ManufacturerBM, AddManufacturerExplanation>> InsertAsync(ManufacturerBM manufacturer) {
            EManufacturer entity = mapper.Map<ManufacturerBM, EManufacturer>(manufacturer);

            try {
                await dbContext.Manufacturers.AddAsync(entity);
                await dbContext.SaveChangesAsync();

                return new SuccessfulResultBM<ManufacturerBM, AddManufacturerExplanation>(
                    mapper.Map<EManufacturer, ManufacturerBM>(entity)
                );
            }
            catch (DbUpdateException) {
                return new FailureResultBM<ManufacturerBM, AddManufacturerExplanation>(AddManufacturerExplanation.DatabaseError);
            }
            catch (Exception) {
                return new FailureResultBM<ManufacturerBM, AddManufacturerExplanation>(AddManufacturerExplanation.GenericError);
            }
        }

        public async Task UpdateAsync(ManufacturerBM manufacturer) {
            EManufacturer entity = await dbContext.Manufacturers.SingleAsync(x => x.Id == manufacturer.ManufacturerId);
            mapper.Map(manufacturer, entity);

            await dbContext.SaveChangesAsync();
        }
    }
}
