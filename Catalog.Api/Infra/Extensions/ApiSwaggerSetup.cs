using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Catalog.Api.Infra.Extensions
{
    public static class ApiSwaggerSetup
    {
        public static void AddApiSwaggerSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }

        public static void UseApiSwaggerSetup(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }
        }
    }

    public class ConfigureSwaggerOptions (IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            return new OpenApiInfo
            {
                Title = $"Catalog API {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "<p>API <strong>documentation</strong> for Catalog.</p>",
                Contact = new OpenApiContact
                {
                    Name = "Renato Souza",
                    Email = "renatozz@gmail.com",
                    Url = new Uri("https://escoladev.com.br")
                }
            }; 
        }
    }
}
