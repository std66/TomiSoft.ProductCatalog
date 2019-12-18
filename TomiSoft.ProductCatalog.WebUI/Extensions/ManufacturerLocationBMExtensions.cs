using System.Globalization;
using TomiSoft.ProductCatalog.BusinessModels;

namespace TomiSoft.ProductCatalog.WebUI.Extensions {
    public static class ManufacturerLocationBMExtensions {
        public static string GetCountryDisplayName(this ManufacturerLocationBM location) {
            string displayName;

            try {
                displayName = new RegionInfo(location.CountryCode).DisplayName;
            }
            catch {
                displayName = location.CountryCode;
            }

            return displayName;
        }
    }
}
