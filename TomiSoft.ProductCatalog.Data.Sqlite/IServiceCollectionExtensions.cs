using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.Data.Sqlite {
    public static class IServiceCollectionExtensions {
        public static IServiceCollection AddSqliteDataManagementLayer(this IServiceCollection services, string connectionString) {
            return services
                .AddEntityFrameworkSqlite()
                .AddDbContext<SqliteProductCatalogDbContext>(
                    (serviceProvider, options) =>
                        options
                            .UseSqlite(connectionString)
                            .UseInternalServiceProvider(serviceProvider)
                )
                .AddScoped<ICategoryDataManager, SqliteCategoryDataManager>()
                .AddScoped<IProductDataManager, SqliteProductDataManager>()
                .AddScoped<IManufacturerDataManager, SqliteManufacturerDataManager>();
        }
    }
}
