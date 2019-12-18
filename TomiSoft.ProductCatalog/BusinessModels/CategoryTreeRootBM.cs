using System.Collections.Generic;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class CategoryTreeRootBM {
        public CategoryTreeRootBM(IEnumerable<CategoryTreeNodeBM> nodes, string languageCode) {
            Nodes = new List<CategoryTreeNodeBM>(nodes);
            LanguageCode = languageCode;
        }

        public IReadOnlyList<CategoryTreeNodeBM> Nodes { get; }
        public string LanguageCode { get; }
    }
}
