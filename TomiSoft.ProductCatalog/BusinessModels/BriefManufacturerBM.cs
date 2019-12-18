using System;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class BriefManufacturerBM {
        public BriefManufacturerBM(int manufacturerId, string name, ManufacturerLocationBM location, Uri websiteUri) {
            ManufacturerId = manufacturerId;
            Name = name;
            Location = location;
            WebsiteUri = websiteUri;
        }

        public int ManufacturerId { get; }
        public string Name { get; }
        public ManufacturerLocationBM Location { get; }
        public Uri WebsiteUri { get; }
    }
}
