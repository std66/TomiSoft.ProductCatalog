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
using TomiSoft.ProductCatalog.Data.Sqlite.Extensions;
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
                    await dbContext.Product.AddAsync(new Product() {
                        Barcode = createProductRequest.Barcode,
                        CategoryId = createProductRequest.CategoryId,
                        ManufacturerId = createProductRequest.ManufacturerId
                    });

                    await dbContext.ProductName.AddRangeAsync(
                        createProductRequest.Name.Select(x => new ProductName() {
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
                Product productToDelete = await dbContext.Product.SingleAsync(x => x.Barcode == barcode.Value);

                dbContext.ProductName.RemoveRange(
                    dbContext.ProductName.Where(x => x.Barcode == barcode.Value)
                );

                dbContext.Product.Remove(productToDelete);

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
            var query = from product in dbContext.Product
                        join productName in dbContext.ProductName on product.Barcode equals productName.Barcode

                        join selected_category in dbContext.Category on product.CategoryId equals selected_category.Id into product_category_join

                        from category in product_category_join.DefaultIfEmpty()
                        join selected_categoryName in dbContext.CategoryName on category.Id equals selected_categoryName.CategoryId into category_categoryname_join

                        from categoryName in category_categoryname_join.DefaultIfEmpty()
                        join selected_manufacturer in dbContext.Manufacturer on product.ManufacturerId equals selected_manufacturer.Id into product_manufacturer_join

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
                                (long?)category.Id ?? 0,
                                categoryName.LanguageCode,
                                categoryName.LocalizedName,
                                category.ParentId
                            ),

                            //manufacturer
                            manufacturer == null ? null : new BriefManufacturerBM(
                                (long?)manufacturer.Id ?? 0,
                                manufacturer.Name,
                                new ManufacturerLocationBM(
                                    manufacturer.LocationCountrycode,
                                    manufacturer.LocationAddress
                                ),
                                manufacturer.WebsiteUrl == null ? null : new Uri(manufacturer.WebsiteUrl)
                            )
                        );

            try {
                return await query.SingleAsync();
            }
            catch (Exception) {
                return null;
            }
        }

        public async Task<IReadOnlyList<LocalizedProductByCategoryBM>> GetLocalizedProductByCategoryAsync(CategoryIdBM categoryId, string languageCode) {
            var query = from product in dbContext.Product

                        join productName in dbContext.ProductName on product.Barcode equals productName.Barcode
                        join manufacturer in dbContext.Manufacturer on product.ManufacturerId equals manufacturer.Id

                        where product.CategoryId == categoryId.Value && productName.LanguageCode == languageCode

                        select new LocalizedProductByCategoryBM(
                            product.Barcode,
                            languageCode,
                            productName.LocalizedName,
                            new BriefManufacturerBM(
                                manufacturer.Id,
                                manufacturer.Name,
                                new ManufacturerLocationBM(manufacturer.LocationCountrycode, manufacturer.LocationAddress),
                                new Uri(manufacturer.WebsiteUrl)
                            )
                        );

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyDictionary<CategoryIdBM, int>> GetNumberOfProductsInCategoriesAsync(params CategoryIdBM[] categoryIds) {
            long[] categoryIdsAsLong = categoryIds.Select(x => x.Value).ToArray();

            var query = from product in dbContext.Product
                        where product.CategoryId.HasValue ? categoryIdsAsLong.Contains(product.CategoryId.Value) : false
                        group product by product.CategoryId into groups
                        select new {
                            Id = groups.Key.Value,
                            Count = groups.Count()
                        };

            var result = await query.ToDictionaryAsync(key => new CategoryIdBM(key.Id), value => value.Count);

            foreach (CategoryIdBM id in categoryIds.Except(result.Keys))
                result.Add(id, 0);

            return result;
        }

        public Task<ProductBM> GetProductAsync(BarcodeBM barcode) {
            var query = from product in dbContext.Product.Include(x => x.ProductName)
                        where product.Barcode == barcode.Value
                        select new ProductBM(
                            product.Barcode,
                            product.ManufacturerId,
                            product.CategoryId,
                            product.ProductName.Select(x => new { Key = x.LanguageCode, Value = x.LocalizedName }).ToDictionary(x => x.Key, x => x.Value)
                        );

            return query.SingleOrDefaultAsync();
        }

        public async Task<ResultBM<IReadOnlyList<BarcodeBM>, GetProductBarcodesByCategoryExplanation>> GetProductBarcodesByCategoryAsync(CategoryIdBM categoryId) {
            try {
                if (!await dbContext.Category.AnyAsync(x => x.Id == categoryId.Value)) {
                    return new FailureResultBM<IReadOnlyList<BarcodeBM>, GetProductBarcodesByCategoryExplanation>(GetProductBarcodesByCategoryExplanation.CategoryNotExists);
                }
            }
            catch (Exception) {
                return new FailureResultBM<IReadOnlyList<BarcodeBM>, GetProductBarcodesByCategoryExplanation>(GetProductBarcodesByCategoryExplanation.DatabaseError);
            }

            List<BarcodeBM> result;

            try {
                result =
                    await dbContext.Product
                        .Where(x => x.CategoryId == categoryId)
                        .Select(x => new BarcodeBM(x.Barcode))
                        .ToListAsync();
            }
            catch (Exception) {
                return new FailureResultBM<IReadOnlyList<BarcodeBM>, GetProductBarcodesByCategoryExplanation>(GetProductBarcodesByCategoryExplanation.DatabaseError);
            }

            return new SuccessfulResultBM<IReadOnlyList<BarcodeBM>, GetProductBarcodesByCategoryExplanation>(result);
        }

        public Task<bool> ProductExistsWithBarcodeAsync(BarcodeBM barcode) {
            return dbContext.Product.AnyAsync(x => x.Barcode == barcode.Value);
        }

        public async Task<bool> UpdateProductAsync(ProductBM product) {
            Product productModel;

            try {
                productModel = await dbContext.Product.Include(x => x.ProductName).SingleAsync(x => x.Barcode == product.Barcode.Value);
            }
            catch (Exception) {
                return false;
            }

            productModel.CategoryId = product.CategoryId;
            productModel.ManufacturerId = product.ManufacturerId;

            productModel.ProductName.RemoveAll(x => !product.ProductName.Keys.Contains(x.LanguageCode));

            foreach (var item in product.ProductName) {
                ProductName productNameModel = productModel.ProductName.FirstOrDefault(x => x.LanguageCode == item.Key);
                if (productNameModel == null) {
                    productModel.ProductName.Add(
                        new ProductName() {
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
