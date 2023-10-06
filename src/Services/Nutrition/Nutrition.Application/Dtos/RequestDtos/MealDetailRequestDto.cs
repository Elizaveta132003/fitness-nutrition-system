using Nutrition.Domain.Enums;

namespace Nutrition.Application.Dtos.RequestDtos
{
    public class MealDetailRequestDto
    {
        // public Guid FoodDiaryId { get; set; }
        public FoodDiaryRequestDto FoodDiary { get; set; }
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
    }
}
