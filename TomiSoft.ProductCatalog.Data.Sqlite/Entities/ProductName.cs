using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities
{
    [Table("product_name")]
    public partial class ProductName
    {
        [Key]
        [Column("barcode")]
        public string Barcode { get; set; }
        [Key]
        [Column("language_code")]
        public string LanguageCode { get; set; }
        [Required]
        [Column("localized_name")]
        public string LocalizedName { get; set; }

        [ForeignKey(nameof(Barcode))]
        [InverseProperty(nameof(Product.ProductName))]
        public virtual Product BarcodeNavigation { get; set; }
    }
}
