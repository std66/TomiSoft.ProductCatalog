using System;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class ManufacturerBM {
        public ManufacturerBM(int manufacturerId, string name, ManufacturerLocationBM location, Uri websiteUri, ManufacturerLogoBM logo) {
            ManufacturerId = manufacturerId;
            Name = name;
            Location = location;
            WebsiteUri = websiteUri;
            Logo = logo;
        }

        public int ManufacturerId { get; }
        public string Name { get; }
        public ManufacturerLocationBM Location { get; }
        public Uri WebsiteUri { get; }
        public ManufacturerLogoBM Logo { get; }
    }
}
