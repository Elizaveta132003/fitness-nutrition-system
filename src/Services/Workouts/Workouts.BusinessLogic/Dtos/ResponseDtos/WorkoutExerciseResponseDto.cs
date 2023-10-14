namespace Workouts.BusinessLogic.Dtos.ResponseDtos
{
    public class WorkoutExerciseResponseDto
    {
        public Guid Id { get; set; }
        public Guid WorkoutId { get; set; }
        public Guid ExerciseId { get; set; }
        public double Duration { get; set; }
    }
}
