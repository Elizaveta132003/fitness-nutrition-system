using Identity.Domain.Entities;
using Identity.Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Configurations
{
    /// <summary>
    /// Configuration for setting up the database and Identity framework.
    /// </summary>
    public static class DatabaseConfigurations
    {
        /// <summary>
        /// Configures the Identity database and services.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        /// <param name="configuration">The configuration containing database connection details.</param>
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

            services.AddIdentity<AppUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
