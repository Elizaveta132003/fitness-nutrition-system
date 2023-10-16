using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;

namespace Nutrition.Infrastructure.Repositories
{
    public class MealDishRepository : BaseRepository<MealDish>, IMealDishRepository
    {
        public MealDishRepository(NutritionDbContext context) : base(context)
        {
        }
    }
}
