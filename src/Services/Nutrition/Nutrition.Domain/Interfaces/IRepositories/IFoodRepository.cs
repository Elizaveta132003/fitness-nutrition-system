using Nutrition.Domain.Entities;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IFoodRepository:IBaseRepository<Food>
    {
        public Task<IEnumerable<Food>> GetAllFoodAsync(CancellationToken cancellationToken = default);
        public Task<Food> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        public Task<bool> FoodExistsAsync(string name, CancellationToken cancellationToken = default);
    }
}
