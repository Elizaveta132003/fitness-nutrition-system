﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nutrition.Application.Interfaces.IGrpcService;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Data.DataContext;
using Nutrition.Infrastructure.Protos;
using Nutrition.Infrastructure.Repositories;
using Nutrition.Infrastructure.Services.GrpcServices;

namespace Nutrition.Infrastructure.Extensions
{
    public static class DatabaseConfigurationExtensions
    {
        public static void ConfigureDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            services.AddDbContext<NutritionDbContext>(options =>
            options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            services.AddScoped<IFoodDiaryRepository, FoodDiaryRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IMealDetailRepository, MealDetailRepository>();
            services.AddScoped<IMealDishRepository, MealDishRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUpdateCaloriesClient, UpdateCaloriesClient>();

            services.AddGrpcClient<CaloriesService.CaloriesServiceClient>(options =>
            {
                var grpcConfig = configuration.GetSection("GrpcConfig");
                options.Address = new Uri(grpcConfig["Url"]!);
            });
        }

        public static void ApplyMigration(this IApplicationBuilder applicationBuilder)
        {
            using var services = applicationBuilder.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<NutritionDbContext>();

            dbContext?.Database.Migrate();
        }
    }
}
