using Nutrition.Domain.Entities;
using Nutrition.Domain.Enums;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IMealDetailRepository : IBaseRepository<MealDetail>
    {
        public Task<bool> MealDetailExistsAsync(DateTime date, MealType mealType, CancellationToken cancellationToken = default);
    }
}
