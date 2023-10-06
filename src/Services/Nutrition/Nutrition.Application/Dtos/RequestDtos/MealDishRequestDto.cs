namespace Nutrition.Application.Dtos.RequestDtos
{
    public class MealDishRequestDto
    {
        public MealDetailRequestDto? MealDetail { get; set; }
        public Guid FoodId { get; set; }
        public double ServingSize { get; set; }
    }
}
