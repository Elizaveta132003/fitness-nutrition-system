namespace Nutrition.Domain.Entities
{
    public class FoodDiary
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
