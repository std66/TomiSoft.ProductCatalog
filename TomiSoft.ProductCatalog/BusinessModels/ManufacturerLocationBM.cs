namespace TomiSoft.ProductCatalog.BusinessModels {
    public class ManufacturerLocationBM {
        public ManufacturerLocationBM(string countryCode, int zipCode, string address) {
            CountryCode = countryCode;
            ZipCode = zipCode;
            Address = address;
        }

        public string CountryCode { get; }
        public int ZipCode { get; }
        public string Address { get; }
    }
}
