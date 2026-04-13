using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace TemplateNet8.Api.Infra.Extensions
{
    public static class ApiHealthCkecksSetup
    {
        public static void AddApiHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();
        }

        public static void UseApiHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => false,

            });
        }
    }
}