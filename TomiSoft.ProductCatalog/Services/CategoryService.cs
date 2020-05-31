using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Services {
    public class CategoryService : ICategoryService {
        private readonly ICategoryDataManager categoryDataManager;
        private readonly IProductDataManager productDataManager;

        public CategoryService(ICategoryDataManager categoryDataManager, IProductDataManager productDataManager) {
            this.categoryDataManager = categoryDataManager;
            this.productDataManager = productDataManager;
        }

        public async Task<CategoryTreeRootBM> GetCategoryTreeAsync(string languageCode) {
            IEnumerable<LocalizedCategoryBM> categories = await categoryDataManager.GetAllAsync(languageCode);

            IReadOnlyDictionary<CategoryIdBM, int> productCount = await productDataManager.GetNumberOfProductsInCategoriesAsync(
                categories.Select(x => x.CategoryId).ToArray()
            );

            IEnumerable<CategoryTreeNodeBM> childNodes = BuildNode(categories, null, productCount);

            return new CategoryTreeRootBM(childNodes, languageCode);
        }

        public Task<LocalizedCategoryBM> GetCategory(int categoryId, string languageCode) {
            return categoryDataManager.GetAsync(categoryId, languageCode);
        }

        public Task<IReadOnlyList<LocalizedCategoryWithProductCountBM>> GetAllCategoriesAsync(string languageCode) {
            return categoryDataManager.GetAllCategoriesWithProductCountAsync(languageCode);
        }

        private IEnumerable<CategoryTreeNodeBM> BuildNode(IEnumerable<LocalizedCategoryBM> categories, CategoryIdBM parentCategoryId, IReadOnlyDictionary<CategoryIdBM, int> productCountInCategories) {
            List<CategoryTreeNodeBM> childNodes = new List<CategoryTreeNodeBM>();

            foreach (LocalizedCategoryBM category in categories.Where(x => x.ParentCategoryId == parentCategoryId)) {
                childNodes.Add(
                    new CategoryTreeNodeBM(
                        category: category,
                        nodes: BuildNode(categories, category.CategoryId, productCountInCategories),
                        productsInCategory: productCountInCategories[category.CategoryId]
                    )    
                );
            }

            return childNodes;
        }
    }
}
