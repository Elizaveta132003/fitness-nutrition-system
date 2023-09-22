using Nutrition.Domain.Entities;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IMealDetailRepository
    {
        public Task CreateAsync(MealDetail mealDetail);
    }
}
