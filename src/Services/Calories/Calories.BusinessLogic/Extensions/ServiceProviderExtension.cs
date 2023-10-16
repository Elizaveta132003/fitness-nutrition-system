using Calories.BusinessLogic.Services.Implementations;
using Calories.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Calories.BusinessLogic.Extensions
{
    public static class ServiceProviderExtension
    {
        public static void AddBusinessLogicService(this IServiceCollection services)
        {
            services.AddScoped<ICaloriesTrackingService, CaloriesTrackingService>();
        }
    }
}
