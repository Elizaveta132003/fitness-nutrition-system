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
            var mongoClient = new MongoClient(configuration["MongoSettings:Connection"]);
            var database = mongoClient.GetDatabase(configuration.GetValue<string>("MongoSettings:DatabaseName"));

            services.AddScoped<IMongoDatabase>(provider => database);

            services.AddScoped<ICaloriesTrackingRepository, CaloriesTrackingRepository>();
        }
    }
}
