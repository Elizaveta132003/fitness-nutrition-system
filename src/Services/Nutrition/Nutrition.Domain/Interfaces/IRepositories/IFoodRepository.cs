using Nutrition.Domain.Entities;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IFoodRepository : IBaseRepository<Food>
    {
        public Task<IEnumerable<Food>> GetAllFoodAsync(CancellationToken cancellationToken = default);
    }
}
