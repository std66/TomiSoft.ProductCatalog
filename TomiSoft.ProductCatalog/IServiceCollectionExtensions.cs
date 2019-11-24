using Microsoft.Extensions.DependencyInjection;

namespace TomiSoft.ProductCatalog {
    public static class IServiceCollectionExtensions {
        public static IServiceCollection AddProductCatalogServices(this IServiceCollection services) {
            return services;
        }
    }
}
