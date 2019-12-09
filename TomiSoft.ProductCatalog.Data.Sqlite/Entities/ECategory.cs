using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities {
    [Table("category")]
    internal class ECategory : IEquatable<ECategory> {
        [Column("id"), Required, Key]
        public int Id { get; set; }

        [Column("parent_id")]
        public int? ParentId { get; set; }

        public override bool Equals(object obj) {
            return Equals(obj as ECategory);
        }

        public bool Equals(ECategory other) {
            return other != null &&
                   Id == other.Id &&
                   ParentId == other.ParentId;
        }

        public override int GetHashCode() {
            var hashCode = -1466526328;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + ParentId.GetHashCode();
            return hashCode;
        }
    }
}
