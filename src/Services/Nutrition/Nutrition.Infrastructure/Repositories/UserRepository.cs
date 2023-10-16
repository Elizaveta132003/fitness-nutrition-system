using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;

namespace Nutrition.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(NutritionDbContext context) : base(context)
        {
        }
    }
}
