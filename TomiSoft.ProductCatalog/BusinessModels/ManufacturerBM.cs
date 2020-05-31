using System;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class ManufacturerBM : BriefManufacturerBM {
        public ManufacturerBM(ManufacturerIdBM manufacturerId, string name, ManufacturerLocationBM location, Uri websiteUri, ManufacturerLogoBM logo)
            : base(manufacturerId, name, location, websiteUri) {
            Logo = logo;
        }

        public ManufacturerLogoBM Logo { get; }
    }
}
