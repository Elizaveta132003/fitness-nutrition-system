using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nutrition.Infrastructure.BackgroundTasks;

namespace Nutrition.Infrastructure.Extensions
{
    public static class HangfireExtensions
    {
        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("HangfireConnection");

            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString));

            services.AddHangfireServer();

            services.AddScoped<ICaloriesCalculationTask, CaloriesCalculationTask>();
        }
    }
}
