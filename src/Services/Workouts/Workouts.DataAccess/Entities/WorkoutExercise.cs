namespace Workouts.DataAccess.Entities
{
    public class WorkoutExercise : BaseEntity
    {
        public Guid WorkoutId { get; set; }
        public Workout? Workout { get; set; }
        public Guid ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
        public double Duration { get; set; }
    }
}
