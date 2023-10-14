using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workouts.DataAccess.Entities;

namespace Workouts.DataAccess.Configuration
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.Property(exercise => exercise.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(exercise => exercise.CaloriesBurnedPerMinute)
                .IsRequired();

            builder.Property(exercise => exercise.ExerciseType)
                .IsRequired();
        }
    }
}
