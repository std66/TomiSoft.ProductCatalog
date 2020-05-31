using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomiSoft.ProductCatalog.Data.Sqlite.Entities
{
    [Table("category")]
    public partial class Category
    {
        public Category()
        {
            CategoryName = new HashSet<CategoryName>();
            Product = new HashSet<Product>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("parent_id")]
        public long? ParentId { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<CategoryName> CategoryName { get; set; }
        [InverseProperty("Category")]
        public virtual ICollection<Product> Product { get; set; }
    }
}
