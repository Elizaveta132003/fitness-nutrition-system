using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Workouts.DataAccess.DataContext;
using Workouts.DataAccess.Entities;

namespace Workouts.DataAccess.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigration(this IApplicationBuilder applicationBuilder)
        {
            using var services = applicationBuilder.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<WorkoutDbContext>();

            dbContext?.Database.Migrate();
            SeedExercises(dbContext!);
        }

        private static void SeedExercises(WorkoutDbContext dbContext)
        {
            if (!dbContext.Exercises.Any())
            {
                var exercises = GetExercises();
                dbContext.Exercises.AddRange(exercises);
                dbContext.SaveChanges();
            }
        }

        private static IEnumerable<Exercise> GetExercises()
        {
            var exercises = new List<Exercise>
            {
                new Exercise
                {
                    Name="Exercise 1",
                    CaloriesBurnedPerMinute=1,
                    ExerciseType=0
                },
                new Exercise
                {
                    Name="Exercise 2",
                    CaloriesBurnedPerMinute=2,
                    ExerciseType=0
                }
            };

            return exercises;
        }
    }
}
