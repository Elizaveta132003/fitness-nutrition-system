using Nutrition.Domain.Entities;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IFoodDiaryRepository
    {
        public Task CreateAsync(FoodDiary foodDiary);
        public void Delete(FoodDiary foodDiary);
    }
}
