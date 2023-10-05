using Microsoft.EntityFrameworkCore;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Enums;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;

namespace Nutrition.Infrastructure.Repositories
{
    public class MealDetailRepository : BaseRepository<MealDetail>, IMealDetailRepository
    {
        private readonly NutritionDbContext _context;
        public MealDetailRepository(NutritionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> MealDetailExistsAsync(DateTime date, MealType mealType,
            CancellationToken cancellationToken = default)
            => await _context.MealDetails.
            AnyAsync(m => m.Date == date && m.MealType == mealType, cancellationToken);
    }
}
