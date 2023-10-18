using Workouts.DataAccess.DataContext;
using Workouts.DataAccess.Entities;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.DataAccess.Repositories.Implementations
{
    public class WorkoutRepository : BaseRepository<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(WorkoutDbContext context) : base(context)
        {
        }
    }
}
