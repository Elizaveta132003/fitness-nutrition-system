using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.API.Configurations
{
    /// <summary>
    /// Configuration class for adding JWT authentication to the application.
    /// </summary>
    public static class AuthenticationConfiguration
    {
        /// <summary>
        /// Adds JWT authentication configuration to the application.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        /// <param name="configuration">The configuration containing JWT settings.</param>
        public static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication
            (
                auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SecretKey").Value!))
                    };
                }
            );
        }
    }
}
