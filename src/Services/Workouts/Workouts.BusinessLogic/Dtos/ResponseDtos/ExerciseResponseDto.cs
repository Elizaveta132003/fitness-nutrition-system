using Workouts.DataAccess.Enums;

namespace Workouts.BusinessLogic.Dtos.ResponseDtos
{
    public class ExerciseResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double CaloriesBurnedPerMinute { get; set; }
        public ExerciseType ExerciseType { get; set; }
    }
}
