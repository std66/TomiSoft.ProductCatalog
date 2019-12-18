namespace TomiSoft.ProductCatalog.BusinessModels {
    public class LocalizedCategoryBM {
        public LocalizedCategoryBM(int categoryId, string languageCode, string localizedName, int? parentCategoryId) {
            CategoryId = categoryId;
            LanguageCode = languageCode;
            LocalizedName = localizedName;
            ParentCategoryId = parentCategoryId;
        }

        public int CategoryId { get; }
        public string LanguageCode { get; }
        public string LocalizedName { get; }
        public int? ParentCategoryId { get; }
    }
}
