namespace Nutrition.Application.Dtos.RequestDtos
{
    public class MealDishRequestDto
    {
        public Guid MealDetailId { get; set; }
        public MealDetailRequestDto? MealDetailRequestDto { get; set; }
        public Guid FoodId { get; set; }
        public double ServingSize { get; set; }
    }
}
