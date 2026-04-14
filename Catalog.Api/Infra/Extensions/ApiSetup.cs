using Catalog.CrossCutting;

namespace Catalog.Api.Infra.Extensions
{
    public static class ApiSetup
    {
        public static void AddApiSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddApiVersioningSetup();
            services.AddEndpointsApiExplorer();
            services.AddApiSwaggerSetup();
            services.AddHealthChecks();
            services.AddCrossCuttingDependenccies(configuration);
        }

        public static void UseApiSetup(this WebApplication app)
        {

            app.UseApiSwaggerSetup();
            app.UseApiHealthChecks();

            app.MapControllers();
        }
    }
}
