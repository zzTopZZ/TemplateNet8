using Catalog.UI.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.UI.Shared.Infra.Extensions
{
    public static class FrontendSetup
    {
        public static void AddFrontSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApiService, ApiService>();
            services.AddHttpClient("Catalog.Api", client =>
            {
                var appUrl = configuration["APIServer:Url"] ?? 
                    throw new Exception("API Server URL not found.");
                client.BaseAddress = new Uri(appUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }
    }
}
