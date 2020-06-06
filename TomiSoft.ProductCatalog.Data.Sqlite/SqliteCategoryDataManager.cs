using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Data.Sqlite {
    internal class SqliteCategoryDataManager : ICategoryDataManager {
        private readonly SqliteProductCatalogDbContext context;

        public SqliteCategoryDataManager(SqliteProductCatalogDbContext context) {
            this.context = context;
        }

        public async Task<IEnumerable<LocalizedCategoryBM>> GetAllAsync(string languageCode) {
            List<LocalizedCategoryBM> result = new List<LocalizedCategoryBM>();

            foreach (var category in await context.Category.ToListAsync()) {
                CategoryName localizedName = await context.CategoryName.SingleAsync(x => x.CategoryId == category.Id && x.LanguageCode == languageCode);

                result.Add(new LocalizedCategoryBM(
                    categoryId: category.Id,
                    languageCode: localizedName.LanguageCode,
                    localizedName: localizedName.LocalizedName,
                    parentCategoryId: category.ParentId
                ));
            }

            return result;
        }

        public async Task<IReadOnlyList<LocalizedCategoryWithProductCountBM>> GetAllCategoriesWithProductCountAsync(string languageCode) {
            return await context.Category
                .Select(x => new LocalizedCategoryWithProductCountBM(
                    new LocalizedCategoryBM(
                        x.Id,
                        languageCode,
                        x.CategoryName.FirstOrDefault(y => y.LanguageCode == languageCode).LocalizedName,
                        x.ParentId == null ? null : new CategoryIdBM(x.ParentId.Value)
                    ),
                    x.Product.Count
                ))
                .ToListAsync();
        }

        public async Task<LocalizedCategoryBM> GetAsync(CategoryIdBM id, string languageCode) {
            Category category = await context.Category.SingleAsync(x => x.Id == id.Value);
            CategoryName name = await context.CategoryName.SingleAsync(x => x.CategoryId == id.Value && x.LanguageCode == languageCode);

            return new LocalizedCategoryBM(
                categoryId: category.Id,
                languageCode: name.LanguageCode,
                localizedName: name.LocalizedName,
                parentCategoryId: category.ParentId
            );
        }
    }
}
