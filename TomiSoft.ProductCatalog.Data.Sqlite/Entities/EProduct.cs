using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities {
    [Table("product")]
    internal class EProduct {
        [Column("barcode"), Key, Required]
        public string Barcode { get; set; }

        [Column("category_id"), Required]
        public int CategoryId { get; set; }

        [Column("manufacturer_id"), Required]
        public int ManufacturerId { get; set; }
    }
}
