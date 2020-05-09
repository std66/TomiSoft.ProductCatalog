using Microsoft.EntityFrameworkCore;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities;

namespace TomiSoft.ProductCatalog.Data.Sqlite {
    internal class SqliteProductCatalogDbContext : DbContext {
        public DbSet<EManufacturer> Manufacturers { get; set; }
        public DbSet<ECategory> Categories { get; set; }
        public DbSet<ECategoryName> CategoryNames { get; set; }
        public DbSet<EProduct> Products { get; set; }
        public DbSet<EProductName> ProductNames { get; set; }

        public SqliteProductCatalogDbContext(DbContextOptions<SqliteProductCatalogDbContext> options)
            : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder
                .Entity<ECategoryName>()
                .HasKey(c => new { c.CategoryId, c.LanguageCode });

            modelBuilder
                .Entity<EProductName>()
                .HasKey(c => new { c.Barcode, c.LanguageCode });
        }
    }
}
