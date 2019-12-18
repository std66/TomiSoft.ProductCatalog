using System.Collections.Generic;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class CategoryTreeNodeBM {
        public CategoryTreeNodeBM(LocalizedCategoryBM category, IEnumerable<CategoryTreeNodeBM> nodes, int productsInCategory) {
            Category = category;
            ProductsInCategory = productsInCategory;
            Nodes = new List<CategoryTreeNodeBM>(nodes);
        }

        public LocalizedCategoryBM Category { get; }
        public int ProductsInCategory { get; }
        public IReadOnlyList<CategoryTreeNodeBM> Nodes { get; }
    }
}
