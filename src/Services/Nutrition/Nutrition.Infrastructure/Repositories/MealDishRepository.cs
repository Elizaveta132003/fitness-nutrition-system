using Microsoft.EntityFrameworkCore;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Enums;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;

namespace Nutrition.Infrastructure.Repositories
{
    public class MealDishRepository : BaseRepository<MealDish>, IMealDishRepository
    {
        private readonly NutritionDbContext _context;
        public MealDishRepository(NutritionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MealDish>> GetAllMealDishesByUserIdAndDateAndMealTypeAsync(
            Guid userId, DateTime date, MealType mealType,
            CancellationToken cancellationToken = default)
            => await _context.MealDishes
            .Where(md => md.MealDetail.FoodDiary.UserId == userId && md.MealDetail.Date == date
            && md.MealDetail.MealType == mealType)
            .ToListAsync(cancellationToken);

        public async Task<IEnumerable<MealDish>> GetAllMealDishesByUserIdAndDateAsync(
            Guid userId, DateTime date, CancellationToken cancellationToken = default)
            => await _context.MealDishes
            .Where(md => md.MealDetail.FoodDiary.UserId == userId && md.MealDetail.Date == date)
            .ToListAsync(cancellationToken);
    }
}
