namespace Nutrition.Domain.Entities
{
    public class Food:BaseEntity
    {
        public string Name { get; set; }
        public double Calories { get; set; }
    }
}