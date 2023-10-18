namespace Workouts.DataAccess.Entities
{
    public class Workout : BaseEntity
    {
        public Guid ExercisesDiaryId { get; set; }
        public ExercisesDiary? ExercisesDiary { get; set; }
        public DateTime Date { get; set; }
        public List<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
