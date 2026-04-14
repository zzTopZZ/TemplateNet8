using Catalog.Application.UseCases.Categories;
using Catalog.Application.UseCases.Products;
using Catalog.Data.Context;
using Catalog.Data.Repositories;
using Catalog.Domain.Contracts.Infra;
using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Excepitions.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Catalog.CrossCutting
{
    public static class DependencyInjectionSetup
    {
        public static void AddCrossCuttingDependenccies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServices();
            services.AddDataServices(configuration);
        }
        private static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
        }

        private static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var connection = configuration.GetConnectionString("DbConnection") ?? throw new ConnectionStringReferenceException("Connection string 'DbConnection' not found.");

            services.AddDbContext<CatalogDbContext>(options =>                
                options.UseSqlServer(connection)

            );
        }
    }
}
