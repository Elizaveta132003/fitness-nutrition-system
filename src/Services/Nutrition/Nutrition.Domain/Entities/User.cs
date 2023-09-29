namespace Nutrition.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public FoodDiary? FoodDiary { get; set; }
    }
}
