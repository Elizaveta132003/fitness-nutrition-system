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

        public async Task<CaloriesTracking> GetByDateAndUserIdAsync(DateTime date, string userId,
            CancellationToken cancellationToken = default)
        {
            var tomorrowDay = date.AddDays(1);

            var filter = Builders<CaloriesTracking>.Filter.And(
                Builders<CaloriesTracking>.Filter.Gte(x => x.Date, date),
                Builders<CaloriesTracking>.Filter.Lt(x => x.Date, tomorrowDay),
                Builders<CaloriesTracking>.Filter.Eq(x => x.UserId, userId));

            var entity = await _mongoCollection.FindAsync(filter,
                cancellationToken: cancellationToken);

            return await entity.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
