namespace TomiSoft.ProductCatalog.BusinessModels {
    public class LocalizedProductBM {
        public LocalizedProductBM(string barcode, string languageCode, string localizedName, LocalizedCategoryBM category, ManufacturerBM manufacturer) {
            Barcode = barcode;
            LanguageCode = languageCode;
            LocalizedName = localizedName;
            Category = category;
            Manufacturer = manufacturer;
        }

        public string Barcode { get; }
        public string LanguageCode { get; }
        public string LocalizedName { get; }
        public LocalizedCategoryBM Category { get; }
        public ManufacturerBM Manufacturer { get; }
    }
}
