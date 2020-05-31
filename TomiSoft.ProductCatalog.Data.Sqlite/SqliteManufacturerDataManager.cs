using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;
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
            dbContext.Manufacturer.Remove(
                await dbContext.Manufacturer.SingleAsync(x => x.Id == manufacturer.ManufacturerId)
            );

            await dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<ManufacturerBM>> GetAllAsync() {
            return await dbContext.Manufacturer
                .ProjectTo<ManufacturerBM>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<BriefManufacturerBM>> GetAllBriefAsync() {
            List<PBriefManufacturer> list = await dbContext.Manufacturer
                .ProjectTo<PBriefManufacturer>(mapper.ConfigurationProvider)
                .ToListAsync();

            return list
                .Select(x => mapper.Map<PBriefManufacturer, BriefManufacturerBM>(x))
                .ToList();
        }

        public async Task<ManufacturerBM> GetAsync(ManufacturerIdBM id) {
            return mapper.Map<Manufacturer, ManufacturerBM>(
                await dbContext.Manufacturer.SingleAsync(x => x.Id == id.Value)
            );
        }

        public async Task<ResultBM<ManufacturerLogoBM, GetManufacturerLogoExplanation>> GetLogoAsync(ManufacturerIdBM id) {
            ManufacturerLogoBM result;

            try {
                var data = await dbContext.Manufacturer.ProjectTo<PManufacturerCompanyLogo>(mapper.ConfigurationProvider).SingleAsync(x => x.Id == id.Value);

                if (data.CompanyLogo == null) {
                    return new FailureResultBM<ManufacturerLogoBM, GetManufacturerLogoExplanation>(GetManufacturerLogoExplanation.ManufacturerLogoNotExists);
                }

                result = new ManufacturerLogoBM(data.CompanyLogo, data.CompanyLogoMimetype);
            }
            catch (InvalidOperationException) {
                return new FailureResultBM<ManufacturerLogoBM, GetManufacturerLogoExplanation>(GetManufacturerLogoExplanation.ManufacturerNotExists);
            }
            catch (Exception) {
                return new FailureResultBM<ManufacturerLogoBM, GetManufacturerLogoExplanation>(GetManufacturerLogoExplanation.DatabaseError);
            }

            return new SuccessfulResultBM<ManufacturerLogoBM, GetManufacturerLogoExplanation>(result);
        }

        public async Task<ResultBM<ManufacturerBM, AddManufacturerExplanation>> InsertAsync(ManufacturerBM manufacturer) {
            Manufacturer entity = mapper.Map<ManufacturerBM, Manufacturer>(manufacturer);

            try {
                await dbContext.Manufacturer.AddAsync(entity);
                await dbContext.SaveChangesAsync();

                return new SuccessfulResultBM<ManufacturerBM, AddManufacturerExplanation>(
                    mapper.Map<Manufacturer, ManufacturerBM>(entity)
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
            Manufacturer entity = await dbContext.Manufacturer.SingleAsync(x => x.Id == manufacturer.ManufacturerId);
            mapper.Map(manufacturer, entity);

            await dbContext.SaveChangesAsync();
        }
    }
}
