using Calories.DataAccess.Repositories.Implementations;
using Calories.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Calories.DataAccess.Extensions
{
    public static class DatabaseConfigurationExtensions
    {
        public static void ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMongoClient>(sp => new MongoClient(configuration["MongoSettings:Connection"]));
            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(configuration.GetValue<string>("MongoSettings:DatabaseName"));
            });

            services.AddScoped<ICaloriesTrackingRepository, CaloriesTrackingRepository>();
        }
    }
}
