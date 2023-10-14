using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Workouts.DataAccess.DataContext;
using Workouts.DataAccess.Entities;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.DataAccess.Repositories.Implementations
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected WorkoutDbContext _context;

        protected BaseRepository(WorkoutDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<T>()
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);

        public async Task<T> GetOneByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
