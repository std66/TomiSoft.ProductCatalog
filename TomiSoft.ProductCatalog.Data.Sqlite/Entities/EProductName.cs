using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities {
    [Table("product_names")]
    class EProductName {
        [Column("barcode"), Key, Required]
        public string Barcode { get; set; }

        [Column("language_code"), Key, Required]
        public string LanguageCode { get; set; }
        
        [Column("localized_name"), Required]
        public string LocalizedName { get; set; }

        public override bool Equals(object obj) {
            return obj is EProductName name &&
                   Barcode == name.Barcode &&
                   LanguageCode == name.LanguageCode &&
                   LocalizedName == name.LocalizedName;
        }

        public override int GetHashCode() {
            var hashCode = -376522774;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Barcode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LanguageCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LocalizedName);
            return hashCode;
        }
    }
}
