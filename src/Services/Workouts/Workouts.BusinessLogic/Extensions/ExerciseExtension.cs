using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Workouts.BusinessLogic.Helpers;
using Workouts.BusinessLogic.Options;
using Workouts.DataAccess.Entities;

namespace Workouts.BusinessLogic.Extensions
{
    public static class ExerciseExtension
    {
        public static async Task AddToCacheAsync(this Exercise exercise, IDistributedCache cache,
            IOptions<RedisCacheOptions> options)
        {
            var cacheKey = CacheHelper.GetCacheKeyForExercise(exercise.Id);

            var cacheOptions = new DistributedCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(options.Value.SlidingExpirationTimeInMinutes)
            };

            await cache.SetAsync(cacheKey, exercise, cacheOptions);
        }
    }
}
