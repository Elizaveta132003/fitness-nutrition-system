using Workouts.DataAccess.Entities;

namespace Workouts.DataAccess.Repositories.Interfaces
{
    public interface IExerciseRepository : IBaseRepository<Exercise>
    {
        public Task<IEnumerable<Exercise>> GetAllExercisesAsync(CancellationToken cancellationToken = default);
    }
}
