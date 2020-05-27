using TomiSoft.ProductCatalog.BusinessModels.Concepts;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class LocalizedProductBM {
        public LocalizedProductBM(BarcodeBM barcode, string languageCode, string localizedName, LocalizedCategoryBM category, BriefManufacturerBM manufacturer) {
            Barcode = barcode;
            LanguageCode = languageCode;
            LocalizedName = localizedName;
            Category = category;
            Manufacturer = manufacturer;
        }

        public BarcodeBM Barcode { get; }
        public string LanguageCode { get; }
        public string LocalizedName { get; }
        public LocalizedCategoryBM Category { get; }
        public BriefManufacturerBM Manufacturer { get; }
    }
}
