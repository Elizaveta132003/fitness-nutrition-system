namespace Workouts.BusinessLogic.Dtos.RequestDtos
{
    public class WorkoutRequestDto
    {
        public ExercisesDiaryRequestDto ExercisesDiary { get; set; }
        public DateTime Date { get; set; }
    }
}
