namespace Workouts.BusinessLogic.Options
{
    public class RedisCacheOptions
    {
        public const string Section = "Redis";

        public int SlidingExpirationTimeInMinutes { get; set; }
    }
}
