using Nutrition.Domain.Enums;

namespace Nutrition.Application.Dtos.RequestDtos
{
    public class MealDetailRequestDto
    {
        public FoodDiaryRequestDto FoodDiary { get; set; }
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
    }
}
