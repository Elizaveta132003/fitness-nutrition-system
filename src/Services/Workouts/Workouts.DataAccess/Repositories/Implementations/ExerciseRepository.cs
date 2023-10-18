using Microsoft.EntityFrameworkCore;
using Workouts.DataAccess.DataContext;
using Workouts.DataAccess.Entities;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.DataAccess.Repositories.Implementations
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(WorkoutDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync(CancellationToken cancellationToken = default)
            => await _context.Exercises
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
