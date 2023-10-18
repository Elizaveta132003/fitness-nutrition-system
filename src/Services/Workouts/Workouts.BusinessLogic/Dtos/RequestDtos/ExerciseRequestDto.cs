using Workouts.DataAccess.Enums;

namespace Workouts.BusinessLogic.Dtos.RequestDtos
{
    public class ExerciseRequestDto
    {
        public string Name { get; set; }
        public double CaloriesBurnedPerMinute { get; set; }
        public ExerciseType ExerciseType { get; set; }
    }
}
