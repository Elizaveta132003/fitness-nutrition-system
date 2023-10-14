using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workouts.DataAccess.Entities;

namespace Workouts.DataAccess.Configuration
{
    public class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
        {
            builder.Property(workoutExercise => workoutExercise.WorkoutId)
                .IsRequired();

            builder.HasOne(workoutExercise => workoutExercise.Workout)
                .WithMany(workout => workout.WorkoutExercises)
                .HasForeignKey(workoutExercise => workoutExercise.WorkoutId);

            builder.Property(workoutExercise => workoutExercise.ExerciseId)
                .IsRequired();

            builder.HasOne(workoutExercise => workoutExercise.Exercise)
                .WithMany()
                .HasForeignKey(workoutExercise => workoutExercise.ExerciseId);

            builder.Property(workoutExercise => workoutExercise.Duration)
                .IsRequired();
        }
    }
}
