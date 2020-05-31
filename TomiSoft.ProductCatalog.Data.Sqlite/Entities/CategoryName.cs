using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities
{
    [Table("category_name")]
    public partial class CategoryName
    {
        [Key]
        [Column("category_id")]
        public long CategoryId { get; set; }
        [Key]
        [Column("language_code")]
        public string LanguageCode { get; set; }
        [Required]
        [Column("localized_name")]
        public string LocalizedName { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("CategoryName")]
        public virtual Category Category { get; set; }
    }
}
