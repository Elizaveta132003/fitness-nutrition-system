namespace Nutrition.Domain.Entities
{
    public class FoodDiary : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public List<MealDetail>? MealDetails { get; set; }
    }
}
