using System.Collections.Generic;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class GetProductsByCategoryResultBM {
        public GetProductsByCategoryResultBM(IReadOnlyList<LocalizedProductByCategoryBM> products, LocalizedCategoryBM category) {
            Products = products;
            Category = category;
        }

        public IReadOnlyList<LocalizedProductByCategoryBM> Products { get; }
        public int Count => Products.Count;
        public LocalizedCategoryBM Category { get; }
    }
}
