namespace TemplateNet8.Api.Infra.Extensions
{
    public static class ApiSetup
    {
        public static void AddApiSetup(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioningSetup();
            services.AddEndpointsApiExplorer();
            services.AddApiSwaggerSetup();
            services.AddHealthChecks();
        }

        public static void UseApiSetup(this WebApplication app)
        {
            if(app.Environment.IsDevelopment())
            {
                app.UseApiSwaggerSetup();
            }
            app.UseApiHealthChecks();

            app.MapControllers();
        }
    }
}
