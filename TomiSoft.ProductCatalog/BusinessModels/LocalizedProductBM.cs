namespace TomiSoft.ProductCatalog.BusinessModels {
    public class LocalizedProductBM {
        public LocalizedProductBM(string barcode, string languageCode, string localizedName, LocalizedCategoryBM category) {
            Barcode = barcode;
            LanguageCode = languageCode;
            LocalizedName = localizedName;
            Category = category;
        }

        public string Barcode { get; }
        public string LanguageCode { get; }
        public string LocalizedName { get; }
        public LocalizedCategoryBM Category { get; }
    }
}
