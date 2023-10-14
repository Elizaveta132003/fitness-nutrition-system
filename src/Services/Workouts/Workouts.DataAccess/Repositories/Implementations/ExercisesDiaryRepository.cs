using Workouts.DataAccess.DataContext;
using Workouts.DataAccess.Entities;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.DataAccess.Repositories.Implementations
{
    public class ExercisesDiaryRepository : BaseRepository<ExercisesDiary>, IExercisesDiaryRepository
    {
        public ExercisesDiaryRepository(WorkoutDbContext context) : base(context)
        {
        }
    }
}
