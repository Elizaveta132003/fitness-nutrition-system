namespace Workouts.DataAccess.Entities
{
    public class ExercisesDiary : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
