namespace Calories.BusinessLogic.Dtos.RequestDtos
{
    public class CaloriesTrackingRequestDto
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public double CaloriesConsumed { get; set; }
        public double CaloriesBurned { get; set; }
    }
}
