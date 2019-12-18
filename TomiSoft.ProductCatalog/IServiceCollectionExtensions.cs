using Microsoft.Extensions.DependencyInjection;
using TomiSoft.ProductCatalog.Services;

namespace TomiSoft.ProductCatalog {
    public static class IServiceCollectionExtensions {
        public static IServiceCollection AddProductCatalogServices(this IServiceCollection services) {
            return services
                .AddScoped<IProductService, ProductService>()
                .AddScoped<ICategoryService, CategoryService>();
        }
    }
}
