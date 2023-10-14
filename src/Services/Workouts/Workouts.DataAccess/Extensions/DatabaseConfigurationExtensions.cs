using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workouts.DataAccess.DataContext;
using Workouts.DataAccess.Repositories.Implementations;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.DataAccess.Extensions
{
    public static class DatabaseConfigurationExtensions
    {
        public static void ApplyMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<WorkoutDbContext>(options =>
            options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IExercisesDiaryRepository, ExercisesDiaryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
            services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        }
    }
}
