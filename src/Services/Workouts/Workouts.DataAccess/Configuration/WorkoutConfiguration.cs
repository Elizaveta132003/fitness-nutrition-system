using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workouts.DataAccess.Entities;

namespace Workouts.DataAccess.Configuration
{
    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.Property(workout => workout.Date)
                .IsRequired();

            builder.Property(workout => workout.ExercisesDiaryId)
                .IsRequired();

            builder.HasOne(workout => workout.ExercisesDiary)
                .WithMany(exerciseDiary => exerciseDiary.Workouts)
                .HasForeignKey(workout => workout.ExercisesDiaryId);

            builder.HasMany(workout => workout.WorkoutExercises)
                .WithOne(workoutExercise => workoutExercise.Workout)
                .HasForeignKey(workoutExercise => workoutExercise.WorkoutId);
        }
    }
}
