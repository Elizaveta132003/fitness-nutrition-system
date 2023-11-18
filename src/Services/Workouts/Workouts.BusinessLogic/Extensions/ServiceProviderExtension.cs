using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workouts.BusinessLogic.Protos;
using Workouts.BusinessLogic.Services.GrpcServices;
using Workouts.BusinessLogic.Services.Implementations;
using Workouts.BusinessLogic.Services.Interfaces;

namespace Workouts.BusinessLogic.Extensions
{
    public static class ServiceProviderExtension
    {
        public static void AddBusinessLogicService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IWorkoutExerciseService, WorkoutExerciseService>();
            services.AddScoped<IUpdateCaloriesClient, UpdateCaloriesClient>();
        }

        public static void ConfigureGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<CaloriesService.CaloriesServiceClient>(options =>
            {
                var grpcConfig = configuration.GetSection("GrpcConfig");
                options.Address = new Uri(grpcConfig["Url"]!);
            });
        }
    }
}
