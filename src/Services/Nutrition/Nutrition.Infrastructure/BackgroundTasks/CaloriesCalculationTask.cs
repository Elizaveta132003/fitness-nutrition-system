using Hangfire;
using Nutrition.Infrastructure.Interfaces;

namespace Nutrition.Infrastructure.BackgroundTasks
{
    public class CaloriesCalculationTask : ICaloriesCalculationTask
    {
        public void ExecuteCaloriesCalculation(string userName)
        {
            RecurringJob.AddOrUpdate<IMyHubHelper>($"ExecuteCaloriesCalculationAsync-{userName}",
                  hubHelper => hubHelper.SendData(userName),
                Cron.Hourly());
        }
    }
}
