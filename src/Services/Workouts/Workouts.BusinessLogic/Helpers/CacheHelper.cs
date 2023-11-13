using Workouts.DataAccess.Entities;

namespace Workouts.BusinessLogic.Helpers
{
    public static class CacheHelper
    {
        public static string GetCacheKeyForExercise(Guid id) => nameof(Exercise) + id;
    }
}
