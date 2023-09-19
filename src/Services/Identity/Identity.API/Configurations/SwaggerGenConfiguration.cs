using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Identity.API.Configurations
{
    /// <summary>
    /// Configuration class for adding Swagger generation settings.
    /// </summary>
    public static class SwaggerGenConfiguration
    {
        /// <summary>
        /// Adds Swagger generation settings to the service collection.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
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
