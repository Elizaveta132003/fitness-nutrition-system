using Workouts.BusinessLogic.Options;

namespace Workouts.API.Configurations
{
    public static class RedisConfiguration
    {
        public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheUrl = configuration["Redis:Url"];

            services.AddStackExchangeRedisCache(redisOptions =>
            {
                redisOptions.Configuration = redisCacheUrl;
            });
        }
    }
}
