using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;

namespace Nutrition.Infrastructure.Repositories
{
    public class FoodDiaryRepository : BaseRepository<FoodDiary>, IFoodDiaryRepository
    {
        public FoodDiaryRepository(NutritionDbContext context) : base(context)
        {
        }
    }
}
