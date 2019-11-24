namespace TomiSoft.ProductCatalog.BusinessModels {
    public class LocalizedCategoryBM {
        public LocalizedCategoryBM(int categoryId, LocalizedCategoryBM parentCategory, string languageCode, string localizedName) {
            CategoryId = categoryId;
            ParentCategory = parentCategory;
            LanguageCode = languageCode;
            LocalizedName = localizedName;
        }

        public int CategoryId { get; }
        public LocalizedCategoryBM ParentCategory { get; }
        public string LanguageCode { get; }
        public string LocalizedName { get; }
    }
}
