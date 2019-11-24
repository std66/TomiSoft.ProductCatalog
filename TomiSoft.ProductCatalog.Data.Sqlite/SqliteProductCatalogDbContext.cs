using Microsoft.EntityFrameworkCore;
using TomiSoft.ProductCatalog.Data.Sqlite.Entities;

namespace TomiSoft.ProductCatalog.Data.Sqlite {
    internal class SqliteProductCatalogDbContext : DbContext {
        public DbSet<EManufacturer> Manufacturers { get; set; }

        public SqliteProductCatalogDbContext(DbContextOptions<SqliteProductCatalogDbContext> options)
            : base(options) {

        }
    }
}
