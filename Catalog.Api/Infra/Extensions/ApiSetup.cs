using Catalog.Api.Infra.Filters;
using Catalog.CrossCutting;
using Microsoft.Net.Http.Headers;

namespace Catalog.Api.Infra.Extensions
{
    public static class ApiSetup
    {
        public static void AddApiSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CustonExceptionFilter));
            });
            services.AddApiVersioningSetup();
            services.AddEndpointsApiExplorer();
            services.AddApiSwaggerSetup();
            services.AddApiHealthChecks(configuration);
            services.AddCrossCuttingDependenccies(configuration);
            services.AddCors();
        }

        public static void UseApiSetup(this WebApplication app)
        {
            app.UseCors(policy => 
            {
                policy.WithOrigins("https://localhost:7196", "http://localhost:5172")
                .AllowAnyMethod()
                .WithHeaders(
                    HeaderNames.ContentType);
                
            });
            app.UseApiSwaggerSetup();
            app.UseApiHealthChecks();

            app.MapControllers();
        }
    }
}
