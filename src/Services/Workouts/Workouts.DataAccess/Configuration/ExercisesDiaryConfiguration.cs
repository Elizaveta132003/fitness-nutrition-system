using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workouts.DataAccess.Entities;

namespace Workouts.DataAccess.Configuration
{
    public class ExercisesDiaryConfiguration : IEntityTypeConfiguration<ExercisesDiary>
    {
        public void Configure(EntityTypeBuilder<ExercisesDiary> builder)
        {
            builder.Property(exerciseDiary => exerciseDiary.UserId)
                .IsRequired();

            builder.HasOne(exerciseDiary => exerciseDiary.User)
                .WithOne()
                .HasForeignKey<ExercisesDiary>(exerciseDiary => exerciseDiary.UserId);
        }
    }
}
