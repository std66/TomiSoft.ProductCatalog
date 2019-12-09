namespace TomiSoft.ProductCatalog.BusinessModels {
    public class LocalizedCategoryBM {
        public LocalizedCategoryBM(int categoryId, string languageCode, string localizedName) {
            CategoryId = categoryId;
            LanguageCode = languageCode;
            LocalizedName = localizedName;
        }

        public int CategoryId { get; }
        public string LanguageCode { get; }
        public string LocalizedName { get; }
    }
}
