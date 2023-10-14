using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Workouts.API.Configurations
{
    public static class SwaggerGenConfiguration
    {
        public static void AddSwaggerGenConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }
    }
}
