using Workouts.DataAccess.Enums;

namespace Workouts.DataAccess.Entities
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public double CaloriesBurnedPerMinute { get; set; }
        public ExerciseType ExerciseType { get; set; }
    }
}
