using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
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

            foreach (var category in await context.Categories.ToListAsync()) {
                ECategoryName localizedName = await context.CategoryNames.SingleAsync(x => x.CategoryId == category.Id && x.LanguageCode == languageCode);

                result.Add(new LocalizedCategoryBM(
                    categoryId: category.Id,
                    languageCode: localizedName.LanguageCode,
                    localizedName: localizedName.LocalizedName
                ));
            }

            return result;
        }

        public async Task<LocalizedCategoryBM> GetAsync(int id, string languageCode) {
            ECategory category = await context.Categories.SingleAsync(x => x.Id == id);
            ECategoryName name = await context.CategoryNames.SingleAsync(x => x.CategoryId == id && x.LanguageCode == languageCode);

            return new LocalizedCategoryBM(
                categoryId: category.Id,
                languageCode: name.LanguageCode,
                localizedName: name.LocalizedName
            );
        }
    }
}
