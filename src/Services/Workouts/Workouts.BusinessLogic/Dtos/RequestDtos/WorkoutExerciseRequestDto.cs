namespace Workouts.BusinessLogic.Dtos.RequestDtos
{
    public class WorkoutExerciseRequestDto
    {
        public WorkoutRequestDto Workout { get; set; }
        public Guid ExerciseId { get; set; }
        public double Duration { get; set; }
    }
}
