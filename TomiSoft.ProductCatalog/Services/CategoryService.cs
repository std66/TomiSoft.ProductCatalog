using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Services {
    public class CategoryService : ICategoryService {
        private readonly ICategoryDataManager categoryDataManager;

        public CategoryService(ICategoryDataManager categoryDataManager) {
            this.categoryDataManager = categoryDataManager;
        }

        public async Task<CategoryTreeRootBM> GetCategoryTreeAsync(string languageCode) {
            IEnumerable<LocalizedCategoryBM> categories = await categoryDataManager.GetAllAsync(languageCode);
            IEnumerable<CategoryTreeNodeBM> childNodes = BuildNode(categories, null);

            return new CategoryTreeRootBM(childNodes, languageCode);
        }

        public Task<LocalizedCategoryBM> GetCategory(int categoryId, string languageCode) {
            return categoryDataManager.GetAsync(categoryId, languageCode);
        }

        private static IEnumerable<CategoryTreeNodeBM> BuildNode(IEnumerable<LocalizedCategoryBM> categories, int? parentCategoryId) {
            List<CategoryTreeNodeBM> childNodes = new List<CategoryTreeNodeBM>();

            foreach (LocalizedCategoryBM category in categories.Where(x => x.ParentCategoryId == parentCategoryId)) {
                childNodes.Add(
                    new CategoryTreeNodeBM(category, BuildNode(categories, category.CategoryId))
                );
            }

            return childNodes;
        }
    }
}
