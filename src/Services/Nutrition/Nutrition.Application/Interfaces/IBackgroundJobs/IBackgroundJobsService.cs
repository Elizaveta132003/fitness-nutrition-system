namespace Nutrition.Application.Interfaces.IBackgroundJobs
{
    public interface IBackgroundJobsService
    {
        Task<double> CalculateCaloriesForUserAndDayAsync(string userName, DateTime curreentDate,
            CancellationToken cancellationToken = default);
    }
}
