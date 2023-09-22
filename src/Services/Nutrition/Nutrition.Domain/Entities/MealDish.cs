namespace Nutrition.Domain.Entities
{
    public class MealDish
    {
        public Guid MealDetailId { get; set; }
        public MealDetail? MealDetail { get; set; }
        public Guid FoodId { get; set; }
        public Food? Food { get; set; }
        public double ServingSize { get; set; }
    }
}