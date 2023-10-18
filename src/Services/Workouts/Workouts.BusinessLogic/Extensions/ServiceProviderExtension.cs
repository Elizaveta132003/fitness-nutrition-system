using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}
