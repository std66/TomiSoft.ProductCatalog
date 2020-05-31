using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities
{
    [Table("manufacturer")]
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("location_countrycode")]
        public string LocationCountrycode { get; set; }
        [Required]
        [Column("location_address")]
        public string LocationAddress { get; set; }
        [Column("website_url")]
        public string WebsiteUrl { get; set; }
        [Column("company_logo")]
        public byte[] CompanyLogo { get; set; }
        [Column("company_logo_mimetype")]
        public string CompanyLogoMimetype { get; set; }

        [InverseProperty("Manufacturer")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
