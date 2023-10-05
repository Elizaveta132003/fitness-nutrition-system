using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nutrition.Application.Features.Food.Queries.GetAllFood;
using Nutrition.Application.Middleware;

namespace Nutrition.Application.Extensions
{
    public static class MediatRExtensions
    {
        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(GetAllFoodQuery).Assembly))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
