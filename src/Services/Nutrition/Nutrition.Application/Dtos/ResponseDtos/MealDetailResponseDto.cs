using Nutrition.Domain.Enums;

namespace Nutrition.Application.Dtos.ResponseDtos
{
    public class MealDetailResponseDto
    {
        public Guid Id { get; set; }
        public Guid FoodDiaryId { get; set; }
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
    }
}
