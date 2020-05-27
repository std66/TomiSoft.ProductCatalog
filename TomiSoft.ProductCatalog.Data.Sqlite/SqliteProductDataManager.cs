using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;
using TomiSoft.ProductCatalog.BusinessModels.Request;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Data.Sqlite {
    internal class SqliteProductDataManager : IProductDataManager {
        private readonly SqliteProductCatalogDbContext dbContext;

        public SqliteProductDataManager(SqliteProductCatalogDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateProductAsync(CreateProductRequestBM createProductRequest) {
            using (IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync()) {
                try {
                    await dbContext.Products.AddAsync(new EProduct() {
                        Barcode = createProductRequest.Barcode,
                        CategoryId = createProductRequest.CategoryId,
                        ManufacturerId = createProductRequest.ManufacturerId
                    });

                    await dbContext.ProductNames.AddRangeAsync(
                        createProductRequest.Name.Select(x => new EProductName() {
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

        public async Task<EmptyResultBM<DeleteProductExplanation>> DeleteProductAsync(BarcodeBM barcode) {
            try {
                EProduct productToDelete = await dbContext.Products.SingleAsync(x => x.Barcode == barcode.Value);

                dbContext.ProductNames.RemoveRange(
                    dbContext.ProductNames.Where(x => x.Barcode == barcode.Value)
                );

                dbContext.Products.Remove(productToDelete);

                await dbContext.SaveChangesAsync();
            }
            catch (InvalidOperationException) {
                return new EmptyResultBM<DeleteProductExplanation>(DeleteProductExplanation.ProductNotExists);
            }
            catch (Exception) {
                return new EmptyResultBM<DeleteProductExplanation>(DeleteProductExplanation.DatabaseError);
            }

            return new EmptyResultBM<DeleteProductExplanation>();
        }

        public async Task<LocalizedProductBM> GetLocalizedProductAsync(BarcodeBM barcode, string languageCode) {
            var query = from product in dbContext.Products
                        join productName in dbContext.ProductNames on product.Barcode equals productName.Barcode

                        join selected_category in dbContext.Categories on product.CategoryId equals selected_category.Id into product_category_join

                        from category in product_category_join.DefaultIfEmpty()
                        join selected_categoryName in dbContext.CategoryNames on category.Id equals selected_categoryName.CategoryId into category_categoryname_join

                        from categoryName in category_categoryname_join.DefaultIfEmpty()
                        join selected_manufacturer in dbContext.Manufacturers on product.ManufacturerId equals selected_manufacturer.Id into product_manufacturer_join

                        from manufacturer in product_manufacturer_join.DefaultIfEmpty()

                        where
                            product.Barcode == barcode.Value &&
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
                        where product.CategoryId.HasValue ? categoryIds.Contains(product.CategoryId.Value) : false
                        group product by product.CategoryId into groups
                        select new {
                            Id = groups.Key.Value,
                            Count = groups.Count()
                        };

            var result = await query.ToDictionaryAsync(key => key.Id, value => value.Count);

            foreach (var id in categoryIds.Except(result.Keys))
                result.Add(id, 0);

            return result;
        }

        public Task<ProductBM> GetProductAsync(BarcodeBM barcode) {
            var query = from product in dbContext.Products.Include(x => x.ProductNames)
                        where product.Barcode == barcode.Value
                        select new ProductBM(
                            product.Barcode,
                            product.ManufacturerId,
                            product.CategoryId,
                            product.ProductNames.Select(x => new { Key = x.LanguageCode, Value = x.LocalizedName }).ToDictionary(x => x.Key, x => x.Value)
                        );

            return query.SingleOrDefaultAsync();
        }

        public Task<bool> ProductExistsWithBarcodeAsync(BarcodeBM barcode) {
            return dbContext.Products.AnyAsync(x => x.Barcode == barcode.Value);
        }

        public async Task<bool> UpdateProductAsync(ProductBM product) {
            EProduct productModel;

            try {
                productModel = await dbContext.Products.Include(x => x.ProductNames).SingleAsync(x => x.Barcode == product.Barcode.Value);
            }
            catch (Exception) {
                return false;
            }

            productModel.CategoryId = product.CategoryId;
            productModel.ManufacturerId = product.ManufacturerId;

            productModel.ProductNames.RemoveAll(x => !product.ProductName.Keys.Contains(x.LanguageCode));

            foreach (var item in product.ProductName) {
                EProductName productNameModel = productModel.ProductNames.FirstOrDefault(x => x.LanguageCode == item.Key);
                if (productNameModel == null) {
                    productModel.ProductNames.Add(
                        new EProductName() {
                            Barcode = product.Barcode,
                            LanguageCode = item.Key,
                            LocalizedName = item.Value
                        }
                    );
                }
                else {
                    productNameModel.LocalizedName = item.Value;
                }
            }

            try {
                await dbContext.SaveChangesAsync();
            }
            catch (Exception) {
                return false;
            }

            return true;
        }
    }
}
