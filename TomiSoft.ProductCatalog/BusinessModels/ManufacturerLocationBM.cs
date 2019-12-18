namespace TomiSoft.ProductCatalog.BusinessModels {
    public class ManufacturerLocationBM {
        public ManufacturerLocationBM(string countryCode, string address) {
            CountryCode = countryCode;
            Address = address;
        }

        public string CountryCode { get; }
        public string Address { get; }
    }
}
