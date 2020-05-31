using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities
{
    [Table("product")]
    public partial class Product
    {
        public Product()
        {
            ProductName = new HashSet<ProductName>();
        }

        [Key]
        [Column("barcode")]
        public string Barcode { get; set; }
        [Column("category_id")]
        public long? CategoryId { get; set; }
        [Column("manufacturer_id")]
        public long? ManufacturerId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Product")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(ManufacturerId))]
        [InverseProperty("Product")]
        public virtual Manufacturer Manufacturer { get; set; }
        [InverseProperty("BarcodeNavigation")]
        public virtual ICollection<ProductName> ProductName { get; set; }
    }
}
