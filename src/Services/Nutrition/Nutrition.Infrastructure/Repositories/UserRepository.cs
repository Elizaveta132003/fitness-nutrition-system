using Microsoft.EntityFrameworkCore;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;

namespace Nutrition.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly NutritionDbContext _context;
        public UserRepository(NutritionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(string name,
            CancellationToken cancellationToken = default)
            => await _context.Users.AnyAsync(u => u.Username == name, cancellationToken);
    }
}
