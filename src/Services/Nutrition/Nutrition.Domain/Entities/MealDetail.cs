using Nutrition.Domain.Enums;

namespace Nutrition.Domain.Entities
{
    public class MealDetail
    {
        public Guid Id { get; set; }

        public Guid FoodDiaryId { get; set; }
        public FoodDiary? FoodDiary { get; set; }
        public DateTime Date {  get; set; } 
        public MealType MealType { get; set; }
    }
}
