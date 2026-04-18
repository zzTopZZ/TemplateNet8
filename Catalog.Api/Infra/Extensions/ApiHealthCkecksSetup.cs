using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Catalog.Api.Infra.Extensions
{
    public static class ApiHealthCkecksSetup
    {
        public static void AddApiHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(
                    configuration.GetConnectionString("DbConnection"), //?? throw new ConnStringNullReferenceException(""),
                    name: "SQL Server",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "db", "sql" }
                );

        }

        public static void UseApiHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => false,

            });

            app.UseHealthChecks("/health/tags/db", new HealthCheckOptions
            {
                Predicate = registration => registration.Tags.Contains("db"),

            });

            app.UseHealthChecks("/health/details", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var result = new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(entry => new
                        {
                            name = entry.Key,
                            status = entry.Value.Status.ToString(),
                            exception = entry.Value.Exception?.Message,
                            duration = entry.Value.Duration.ToString()
                        })
                    };
                    await context.Response.WriteAsJsonAsync(result);
                }

            });
        }
    }
}