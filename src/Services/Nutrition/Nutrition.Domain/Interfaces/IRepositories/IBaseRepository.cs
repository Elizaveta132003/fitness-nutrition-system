using Nutrition.Domain.Entities;
using System.Linq.Expressions;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetOneByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
