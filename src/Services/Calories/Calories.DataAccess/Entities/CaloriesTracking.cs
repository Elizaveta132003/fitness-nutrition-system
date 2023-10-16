namespace Calories.DataAccess.Entities
{
    public class CaloriesTracking : BaseEntity
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public double CaloriesConsumed { get; set; } = 0;
        public double CaloriesBurned { get; set; } = 0;
    }
}
