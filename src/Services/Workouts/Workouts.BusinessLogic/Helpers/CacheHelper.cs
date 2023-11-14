using Workouts.DataAccess.Entities;

namespace Workouts.BusinessLogic.Helpers
{
    public static class CacheHelper
    {
        public static string GetCacheKeyForExercise(Guid id)
        {
            var key = nameof(Exercise) + id;

            return key;
        }

        public static string GetCacheKeyForAllExercises()
        {
            var key = "exercises";

            return key;
        }
    }
}
