using Microsoft.EntityFrameworkCore;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities;

namespace TomiSoft.ProductCatalog.Data.Sqlite {
    internal class SqliteProductCatalogDbContext : DbContext {
        public DbSet<EManufacturer> Manufacturers { get; set; }
        public DbSet<ECategory> Categories { get; set; }
        public DbSet<ECategoryName> CategoryNames { get; set; }
        public DbSet<EProduct> Products { get; set; }

        public SqliteProductCatalogDbContext(DbContextOptions<SqliteProductCatalogDbContext> options)
            : base(options) {

        }
    }
}
