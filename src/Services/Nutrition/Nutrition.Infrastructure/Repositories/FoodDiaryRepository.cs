using Microsoft.EntityFrameworkCore;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;

namespace Nutrition.Infrastructure.Repositories
{
    public class FoodDiaryRepository : BaseRepository<FoodDiary>, IFoodDiaryRepository
    {
        private readonly NutritionDbContext _context;
        public FoodDiaryRepository(NutritionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FoodDiary> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
            => await _context.FoodDiaries
            .AsNoTracking()
            .FirstOrDefaultAsync(fd => fd.UserId == userId, cancellationToken);
    }
}
