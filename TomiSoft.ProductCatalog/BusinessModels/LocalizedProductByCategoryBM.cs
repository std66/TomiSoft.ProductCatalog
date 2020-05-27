using TomiSoft.ProductCatalog.BusinessModels.Concepts;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class LocalizedProductByCategoryBM {
        public LocalizedProductByCategoryBM(BarcodeBM barcode, string languageCode, string localizedName, BriefManufacturerBM manufacturer) {
            Barcode = barcode;
            LanguageCode = languageCode;
            LocalizedName = localizedName;
            Manufacturer = manufacturer;
        }

        public BarcodeBM Barcode { get; }
        public string LanguageCode { get; }
        public string LocalizedName { get; }
        public BriefManufacturerBM Manufacturer { get; }
    }
}
