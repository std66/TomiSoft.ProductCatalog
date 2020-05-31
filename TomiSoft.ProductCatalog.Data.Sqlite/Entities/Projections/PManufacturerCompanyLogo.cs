namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities.Projections {
    class PManufacturerCompanyLogo {
        public long Id { get; set; }
        public byte[] CompanyLogo { get; set; }
        public string CompanyLogoMimetype { get; set; }
    }
}
