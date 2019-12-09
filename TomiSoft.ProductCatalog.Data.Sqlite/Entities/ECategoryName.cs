using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities {
    [Table("category_names")]
    internal class ECategoryName : IEquatable<ECategoryName> {
        [Column("category_name_id"), Required, Key]
        public int CategoryNameId { get; set; }

        [Column("category_id"), Required]
        public int CategoryId { get; set; }

        [Column("language_code"), Required]
        public string LanguageCode { get; set; }

        [Column("localized_name"), Required]
        public string LocalizedName { get; set; }

        public override bool Equals(object obj) {
            return Equals(obj as ECategoryName);
        }

        public bool Equals(ECategoryName other) {
            return other != null &&
                   CategoryNameId == other.CategoryNameId &&
                   CategoryId == other.CategoryId &&
                   LanguageCode == other.LanguageCode &&
                   LocalizedName == other.LocalizedName;
        }

        public override int GetHashCode() {
            var hashCode = 514451606;
            hashCode = hashCode * -1521134295 + CategoryNameId.GetHashCode();
            hashCode = hashCode * -1521134295 + CategoryId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LanguageCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LocalizedName);
            return hashCode;
        }
    }
}
