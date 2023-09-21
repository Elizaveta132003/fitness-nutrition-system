using Identity.Domain.Entities;
using Identity.Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Configurations
{
    /// <summary>
    /// Configuration class for applying database migrations and seeding data.
    /// </summary>
    public static class DataSeederConfiguration
    {
        /// <summary>
        /// Applies database migrations and seeds data for the specified context.
        /// </summary>
        /// <param name="app">The application host.</param>
        public static void ApplyMigrations(this IHost app)
        {
            app.MigrateDatabase<ApplicationDbContext>(async (context, serviceProvider) =>
             {
                 var dataSeeder = new ApplicationContextSeed(serviceProvider.GetRequiredService<UserManager<AppUser>>(),
                     serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>());
                 await dataSeeder.SeedAsync();
             });
        }

        /// <summary>
        /// Applies database migrations and seeds data for the specified context.
        /// </summary>
        /// <typeparam name="TContext">The type of the DbContext for which migrations and data seeding should be applied.</typeparam>
        /// <param name="host">The application host.</param>
        /// <param name="seeder">An action to perform migration and seeding tasks on the DbContext.</param>
        /// <returns></returns>
        private static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
            where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();

                seeder(context, services);
            }

            return host;
        }
    }
}