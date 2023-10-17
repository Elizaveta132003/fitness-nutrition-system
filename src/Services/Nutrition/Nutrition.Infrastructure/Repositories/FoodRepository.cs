using Microsoft.EntityFrameworkCore;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;

namespace Nutrition.Infrastructure.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        private readonly NutritionDbContext _context;
        public FoodRepository(NutritionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Food>> GetAllFoodAsync(CancellationToken cancellationToken = default)
            => await _context.Foods
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
