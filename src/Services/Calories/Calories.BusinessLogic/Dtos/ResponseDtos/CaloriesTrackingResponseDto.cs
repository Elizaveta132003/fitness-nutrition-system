namespace Calories.BusinessLogic.Dtos.ResponseDtos
{
    public class CaloriesTrackingResponseDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public double CaloriesConsumed { get; set; }
        public double CaloriesBurned { get; set; }
    }
}
