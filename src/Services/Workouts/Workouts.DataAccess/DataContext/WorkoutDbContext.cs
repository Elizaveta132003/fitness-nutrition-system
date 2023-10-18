using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Workouts.DataAccess.Entities;

namespace Workouts.DataAccess.DataContext
{
    public class WorkoutDbContext : DbContext
    {
        public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Exercise> Exercises { get; set; }
        public virtual DbSet<ExercisesDiary> ExercisesDiaries { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<WorkoutExercise> WorkoutsExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
