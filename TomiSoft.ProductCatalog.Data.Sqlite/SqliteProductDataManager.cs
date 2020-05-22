using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Request;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Data.Sqlite {
    internal class SqliteProductDataManager : IProductDataManager {
        private readonly SqliteProductCatalogDbContext dbContext;

        public SqliteProductDataManager(SqliteProductCatalogDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateProduct(CreateProductRequestBM createProductRequest) {
            using (IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync()) {
                try {
                    await dbContext.Products.AddAsync(new Entities.EProduct() {
                        Barcode = createProductRequest.Barcode,
                        CategoryId = createProductRequest.CategoryId,
                        ManufacturerId = createProductRequest.ManufacturerId
                    });

                    await dbContext.ProductNames.AddRangeAsync(
                        createProductRequest.Name.Select(x => new Entities.EProductName() {
                            Barcode = createProductRequest.Barcode,
                            LanguageCode = x.Key,
                            LocalizedName = x.Value
                        })
                    );

                    await dbContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception) {
                    transaction.Rollback();
                    return false;
                }
            }

            return true;
        }
        
        public async Task<LocalizedProductBM> GetLocalizedProductAsync(string barcode, string languageCode) {
            var query = from product in dbContext.Products
                        join productName in dbContext.ProductNames on product.Barcode equals productName.Barcode

                        join selected_category in dbContext.Categories on product.CategoryId equals selected_category.Id into product_category_join

                        from category in product_category_join.DefaultIfEmpty()
                        join selected_categoryName in dbContext.CategoryNames on category.Id equals selected_categoryName.CategoryId into category_categoryname_join

                        from categoryName in category_categoryname_join.DefaultIfEmpty()
                        join selected_manufacturer in dbContext.Manufacturers on product.ManufacturerId equals selected_manufacturer.Id into product_manufacturer_join

                        from manufacturer in product_manufacturer_join.DefaultIfEmpty()

                        where
                            product.Barcode == barcode &&
                            productName.LanguageCode == languageCode &&
                            (categoryName == null ? true : categoryName.LanguageCode == languageCode)

                        select new LocalizedProductBM(
                            //product
                            barcode,
                            languageCode,
                            productName.LocalizedName,

                            //category
                            category == null ? null : new LocalizedCategoryBM(
                                (int?)category.Id ?? 0,
                                categoryName.LanguageCode,
                                categoryName.LocalizedName,
                                category.ParentId
                            ),

                            //manufacturer
                            manufacturer == null ? null : new BriefManufacturerBM(
                                (int?)manufacturer.Id ?? 0,
                                manufacturer.Name,
                                new ManufacturerLocationBM(
                                    manufacturer.CountryCode,
                                    manufacturer.Address
                                ),
                                new Uri(manufacturer.WebsiteUri)
                            )
                        );

            try {
                return await query.SingleAsync();
            }
            catch (Exception) {
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

        public Task<bool> ProductExistsWithBarcode(string barcode) {
            return dbContext.Products.AnyAsync(x => x.Barcode == barcode);
        }
    }
}
