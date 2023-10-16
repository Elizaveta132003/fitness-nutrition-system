using Calories.DataAccess.Entities;
using Calories.DataAccess.Repositories.Interfaces;
using MongoDB.Driver;

namespace Calories.DataAccess.Repositories.Implementations
{
    public class CaloriesTrackingRepository : BaseRepository<CaloriesTracking>, ICaloriesTrackingRepository
    {
        public CaloriesTrackingRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
