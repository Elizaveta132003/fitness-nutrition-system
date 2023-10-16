using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;

namespace Nutrition.Infrastructure.Repositories
{
    public class MealDetailRepository : BaseRepository<MealDetail>, IMealDetailRepository
    {
        public MealDetailRepository(NutritionDbContext context) : base(context)
        {
        }
    }
}
