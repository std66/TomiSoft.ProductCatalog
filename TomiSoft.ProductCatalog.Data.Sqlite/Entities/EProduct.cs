using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities {
    [Table("product")]
    internal class EProduct {
        [Column("barcode"), Key, Required]
        public string Barcode { get; set; }

        [Column("category_id")]
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }

        [Column("manufacturer_id")]
        public int? ManufacturerId { get; set; }

        public ECategory Category { get; set; }

        [ForeignKey(nameof(ManufacturerId))]
        public EManufacturer Manufacturer { get; set; }

        [ForeignKey(nameof(Barcode))]
        public List<EProductName> ProductNames { get; set; }
    }
}
