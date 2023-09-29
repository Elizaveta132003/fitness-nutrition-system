using Nutrition.Domain.Entities;
using Nutrition.Domain.Enums;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IMealDishRepository:IBaseRepository<MealDish>
    {
        public Task<IEnumerable<MealDish>> GetAllMealDishesByUserIdAndDateAsync
            (Guid userId, DateTime date, CancellationToken cancellationToken = default);
        public Task<IEnumerable<MealDish>> GetAllMealDishesByUserIdAndDateAndMealTypeAsync
            (Guid userId, DateTime date, MealType mealType, CancellationToken cancellationToken = default);
    }
}
