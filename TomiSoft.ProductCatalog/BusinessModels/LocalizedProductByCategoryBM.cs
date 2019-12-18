namespace TomiSoft.ProductCatalog.BusinessModels {
    public class LocalizedProductByCategoryBM {
        public LocalizedProductByCategoryBM(string barcode, string languageCode, string localizedName, BriefManufacturerBM manufacturer) {
            Barcode = barcode;
            LanguageCode = languageCode;
            LocalizedName = localizedName;
            Manufacturer = manufacturer;
        }

        public string Barcode { get; }
        public string LanguageCode { get; }
        public string LocalizedName { get; }
        public BriefManufacturerBM Manufacturer { get; }
    }
}
