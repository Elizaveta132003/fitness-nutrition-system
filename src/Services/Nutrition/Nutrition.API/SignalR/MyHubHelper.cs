using Microsoft.AspNetCore.SignalR;
using Nutrition.API.Hubs;
using Nutrition.Application.Interfaces.IBackgroundJobs;
using Nutrition.Infrastructure.Interfaces;

namespace Nutrition.API.SignalR
{
    public class MyHubHelper : IMyHubHelper
    {
        private readonly IHubContext<CaloriesHub> _hubContext;
        private readonly IBackgroundJobsService _backgroundJobsService;

        public MyHubHelper(IHubContext<CaloriesHub> hubContext, IBackgroundJobsService backgroundJobsService)
        {
            _hubContext = hubContext;
            _backgroundJobsService = backgroundJobsService;
        }

        public async Task SendData(string userName)
        {
            var currentDate = DateTime.Now;
            var result = await _backgroundJobsService.CalculateCaloriesForUserAndDayAsync(userName, currentDate);

            await _hubContext.Clients.All.SendAsync("ReceiveCalories", result);
        }
    }
}
