using Nutrition.Domain.Entities;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IFoodRepository
    {
        public Task CreateAsync(Food food);
        public void Update(Food food);  
        public void Delete(Food food);  
        public Task<IEnumerable<Food>> GetAllFoodAsync();
        public Task<Food> GetByNameAsync(string name);
    }
}
