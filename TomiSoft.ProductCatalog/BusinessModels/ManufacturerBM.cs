using System;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class ManufacturerBM : BriefManufacturerBM {
        public ManufacturerBM(int manufacturerId, string name, ManufacturerLocationBM location, Uri websiteUri, ManufacturerLogoBM logo)
            : base(manufacturerId, name, location, websiteUri) {
            Logo = logo;
        }

        public ManufacturerLogoBM Logo { get; }
    }
}
