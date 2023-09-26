using Microsoft.EntityFrameworkCore;
using Nutrition.Domain.Entities;
using System.Reflection;

namespace Nutrition.Infrastructure.Data.DataContext
{
    public class NutritionDbContext:DbContext
    {
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodDiary> FoodDiarys { get;set; }
        public virtual DbSet<MealDetail> MealDetails { get; set; }
        public virtual DbSet<MealDish> MealDishes { get; set;}
        public virtual DbSet<User> Users { get; set; }

        public NutritionDbContext(DbContextOptions<NutritionDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
