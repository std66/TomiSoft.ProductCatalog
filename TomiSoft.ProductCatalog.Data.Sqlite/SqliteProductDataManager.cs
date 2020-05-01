using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Data.Sqlite {
    internal class SqliteProductDataManager : IProductDataManager {
        private readonly SqliteProductCatalogDbContext dbContext;

        public SqliteProductDataManager(SqliteProductCatalogDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<LocalizedProductBM> GetLocalizedProductAsync(string barcode, string languageCode) {
            var query = from product in dbContext.Products

                    join productName in dbContext.ProductNames on product.Barcode equals productName.Barcode
                    join category in dbContext.Categories on product.CategoryId equals category.Id
                    join category_name in dbContext.CategoryNames on category.Id equals category_name.CategoryId
                    join manufacturer in dbContext.Manufacturers on product.ManufacturerId equals manufacturer.Id

                    where product.Barcode == barcode && category_name.LanguageCode == languageCode

                    select new LocalizedProductBM(
                        barcode,
                        languageCode,
                        productName.LocalizedName,
                        new LocalizedCategoryBM(
                            category.Id,
                            languageCode,
                            category_name.LocalizedName,
                            category.ParentId
                        ),
                        new BriefManufacturerBM(
                            manufacturer.Id,
                            manufacturer.Name,
                            new ManufacturerLocationBM(manufacturer.CountryCode, manufacturer.Address),
                            new Uri(manufacturer.WebsiteUri)
                        )
                    );

            try {
                return await query.SingleAsync();
            }
            catch {
                return null;
            }
        }

        public async Task<IReadOnlyList<LocalizedProductByCategoryBM>> GetLocalizedProductByCategoryAsync(int categoryId, string languageCode) {
            var query = from product in dbContext.Products

                        join productName in dbContext.ProductNames on product.Barcode equals productName.Barcode
                        join manufacturer in dbContext.Manufacturers on product.ManufacturerId equals manufacturer.Id

                        where product.CategoryId == categoryId && productName.LanguageCode == languageCode

                        select new LocalizedProductByCategoryBM(
                            product.Barcode,
                            languageCode,
                            productName.LocalizedName,
                            new BriefManufacturerBM(
                                manufacturer.Id,
                                manufacturer.Name,
                                new ManufacturerLocationBM(manufacturer.CountryCode, manufacturer.Address),
                                new Uri(manufacturer.WebsiteUri)
                            )
                        );

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyDictionary<int, int>> GetNumberOfProductsInCategoriesAsync(params int[] categoryIds) {
            var query = from product in dbContext.Products
                        where categoryIds.Contains(product.CategoryId)
                        group product by product.CategoryId into groups
                        select new {
                            Id = groups.Key,
                            Count = groups.Count()
                        };

            var result = await query.ToDictionaryAsync(key => key.Id, value => value.Count);

            foreach (var id in categoryIds.Except(result.Keys))
                result.Add(id, 0);

            return result;
        }
    }
}
