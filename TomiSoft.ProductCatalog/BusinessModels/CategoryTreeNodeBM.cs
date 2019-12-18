using System.Collections.Generic;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class CategoryTreeNodeBM {
        public CategoryTreeNodeBM(LocalizedCategoryBM category, IEnumerable<CategoryTreeNodeBM> nodes) {
            Category = category;
            Nodes = new List<CategoryTreeNodeBM>(nodes);
        }

        public LocalizedCategoryBM Category { get; }
        public IReadOnlyList<CategoryTreeNodeBM> Nodes { get; }
    }
}
