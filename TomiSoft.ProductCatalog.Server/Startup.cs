using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TomiSoft.ProductCatalog.Data.Sqlite;
using TomiSoft.ProductCatalog.Server.Middleware;

namespace TomiSoft.ProductCatalog.Server {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services
                .AddAutoMapper(typeof(Startup), typeof(Data.Sqlite.IServiceCollectionExtensions))
                .AddProductCatalogServices()
                .AddSqliteDataManagementLayer(Configuration["ConnectionStrings:DefaultConnection"])
                .AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();
            
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
