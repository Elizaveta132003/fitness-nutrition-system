using Microsoft.EntityFrameworkCore;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;

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
            => _context.Add(entity);

        public void Delete(T entity)
            => _context.Remove(entity);

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        public void Update(T entity)
            => _context.Update(entity);
    }
}
