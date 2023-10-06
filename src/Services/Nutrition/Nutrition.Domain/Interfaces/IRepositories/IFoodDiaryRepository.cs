using Nutrition.Domain.Entities;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IFoodDiaryRepository : IBaseRepository<FoodDiary>
    {
        public Task<FoodDiary> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
