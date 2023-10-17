using Microsoft.EntityFrameworkCore;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;
using System.Linq.Expressions;

namespace Nutrition.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly NutritionDbContext _context;

        public BaseRepository(NutritionDbContext context)
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
