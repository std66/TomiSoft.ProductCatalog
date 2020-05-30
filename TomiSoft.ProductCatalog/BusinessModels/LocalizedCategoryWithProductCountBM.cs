namespace TomiSoft.ProductCatalog.BusinessModels {
    public class LocalizedCategoryWithProductCountBM : LocalizedCategoryBM {
        public LocalizedCategoryWithProductCountBM(LocalizedCategoryBM category, int productCount)
            : base(category.CategoryId, category.LanguageCode, category.LocalizedName, category.ParentCategoryId) {
            ProductCount = productCount;
        }

        public int ProductCount { get; }
    }
}
