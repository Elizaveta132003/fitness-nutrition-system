namespace Nutrition.Application.Dtos.ResponseDtos
{
    public class MealDishResponseDto
    {
        public Guid Id { get; set; }
        public Guid MealDetailId { get; set; }
        public Guid FoodId { get; set; }
        public double ServingSize { get; set; }
    }
}
