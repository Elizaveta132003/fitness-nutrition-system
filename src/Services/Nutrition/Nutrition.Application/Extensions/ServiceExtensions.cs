using Microsoft.Extensions.DependencyInjection;
using Nutrition.Application.Interfaces.IBackgroundJobs;
using Nutrition.Application.Services;

namespace Nutrition.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IBackgroundJobsService, BackgroundJobsService>();
        }
    }
}
