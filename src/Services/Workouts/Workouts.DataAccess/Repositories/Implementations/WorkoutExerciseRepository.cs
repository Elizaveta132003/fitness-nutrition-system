using Workouts.DataAccess.DataContext;
using Workouts.DataAccess.Entities;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.DataAccess.Repositories.Implementations
{
    public class WorkoutExerciseRepository : BaseRepository<WorkoutExercise>, IWorkoutExerciseRepository
    {
        public WorkoutExerciseRepository(WorkoutDbContext context) : base(context)
        {
        }
    }
}
