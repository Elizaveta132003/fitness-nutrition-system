using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Identity.Infrastructure.DataContext
{
    /// <summary>
    /// Application database context that links Identity entities to the database.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the ApplicationDbContext class.
        /// </summary>
        /// <param name="options">The DbContext options.</param>
        /// <param name="configuration">The configuration containing database connection details.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configures the database context using entity configurations from the current assembly.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Configures the database connection using the connection string from configuration.
        /// </summary>
        /// <param name="optionsBuilder">The DbContext options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}