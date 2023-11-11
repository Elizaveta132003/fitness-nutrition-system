namespace Nutrition.Infrastructure.BackgroundTasks
{
    public interface ICaloriesCalculationTask
    {
        void ExecuteCaloriesCalculation(string userName);
    }
}
