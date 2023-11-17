using Calories.BusinessLogic.Protos;
using Calories.DataAccess.Entities;
using Calories.DataAccess.Repositories.Interfaces;
using Grpc.Core;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Calories.BusinessLogic.Services.GrpcServices
{
    public class UpdateCaloriesService : CaloriesService.CaloriesServiceBase
    {
        private readonly ICaloriesTrackingRepository _caloriesTrackingRepository;
        private readonly ILogger<UpdateCaloriesService> _logger;

        public UpdateCaloriesService(ICaloriesTrackingRepository repository, ILogger<UpdateCaloriesService> logger)
        {
            _caloriesTrackingRepository = repository;
            _logger = logger;
        }

        public override async Task<UpdateCaloriesResponse> UpdateCalories(UpdateCaloriesRequest updateCaloriesRequest,
            ServerCallContext serverCallContext)
        {
            var date = updateCaloriesRequest.Date.ToDateTime().Date;
            var existingCaloriesTracking = await _caloriesTrackingRepository.GetByDateAndUserIdAsync(date, updateCaloriesRequest.UserId);

            if (existingCaloriesTracking is null)
            {
                var createCaloriesResponse = await CreateCaloriesTracking(updateCaloriesRequest);

                return createCaloriesResponse;
            }

            if (updateCaloriesRequest.IsCaloriesConsumed)
            {
                existingCaloriesTracking.CaloriesConsumed += updateCaloriesRequest.Calories;
            }
            else
            {
                existingCaloriesTracking.CaloriesBurned += updateCaloriesRequest.Calories;
            }


            await _caloriesTrackingRepository.UpdateAsync(existingCaloriesTracking);

            _logger.LogInformation($"Calories treacking with id {existingCaloriesTracking.Id} was successfully updated");

            var updateCaloriesResponse = updateCaloriesRequest.Adapt<UpdateCaloriesResponse>();

            return updateCaloriesResponse;
        }

        private async Task<UpdateCaloriesResponse> CreateCaloriesTracking(UpdateCaloriesRequest updateCaloriesRequest)
        {
            var caloriesTracking = new CaloriesTracking
            {
                UserId = updateCaloriesRequest.UserId,
                Date = updateCaloriesRequest.Date.ToDateTime(),
            };

            if (updateCaloriesRequest.IsCaloriesConsumed)
            {
                caloriesTracking.CaloriesConsumed = updateCaloriesRequest.Calories;
            }
            else
            {
                caloriesTracking.CaloriesBurned = updateCaloriesRequest.Calories;
            }


            await _caloriesTrackingRepository.CreateAsync(caloriesTracking);

            _logger.LogInformation($"Calories treacking with id {caloriesTracking.Id} was successfully created");

            var updateCaloriesResponse = updateCaloriesRequest.Adapt<UpdateCaloriesResponse>();

            return updateCaloriesResponse;
        }
    }
}
