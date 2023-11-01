using Calories.DataAccess.Entities;

namespace Calories.DataAccess.Repositories.Interfaces
{
    public interface ICaloriesTrackingRepository : IBaseRepository<CaloriesTracking>
    {
        Task<CaloriesTracking> GetByDateAndUserIdAsync(DateTime date, string userId, CancellationToken cancellationToken = default);
    }
}
