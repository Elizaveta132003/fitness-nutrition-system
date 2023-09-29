using Nutrition.Domain.Enums;

namespace Nutrition.Domain.Entities
{
    public class MealDetail : BaseEntity
    {
        public Guid FoodDiaryId { get; set; }
        public FoodDiary? FoodDiary { get; set; }
        public DateTime Date {  get; set; } 
        public MealType MealType { get; set; }
        public List<MealDish>? MealDishes { get; set; }
    }
}
