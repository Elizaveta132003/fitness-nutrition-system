using Identity.Application.Services.Implementations;
using Identity.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Configurations
{
    /// <summary>
    /// Configuration class for setting up application services.
    /// </summary>
    public static class ServicesConfiguration
    {
        /// <summary>
        /// Configures and adds application services to the service collection.
        /// </summary>
        /// <param name="services">The service collection to which services are added.</param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
