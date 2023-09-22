using Nutrition.Domain.Entities;
using Nutrition.Domain.Enums;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IMealDishRepository
    {
        public Task CreateAsync(MealDish mealDish);
        public void Update(MealDish mealDish);
        public void Delete(MealDish mealDish);
        public Task<IEnumerable<MealDish>> GetAllMealDishesByUserIdAndDateAsync(Guid userId, DateTime date);
        public Task<IEnumerable<MealDish>> GetAllMealDishesByUserIdAndDateAndMealTypeAsync
            (Guid userId, DateTime date, MealType mealType);
    }
}
