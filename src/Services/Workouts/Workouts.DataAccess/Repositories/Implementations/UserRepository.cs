using Workouts.DataAccess.DataContext;
using Workouts.DataAccess.Entities;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.DataAccess.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(WorkoutDbContext context) : base(context)
        {
        }
    }
}
