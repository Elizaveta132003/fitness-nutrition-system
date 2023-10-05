using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;
using Nutrition.Infrastructure.Repositories;

namespace Nutrition.Infrastructure.Extensions
{
    public static class DatabaseConfigurationExtensions
    {
        public static void ApplyMigrations(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<NutritionDbContext>(options =>
            options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            //services.AddScoped(IBaseRepository, BaseRepository);
            services.AddScoped<IFoodDiaryRepository, FoodDiaryRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IMealDetailRepository, MealDetailRepository>();
            services.AddScoped<IMealDishRepository, MealDishRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
