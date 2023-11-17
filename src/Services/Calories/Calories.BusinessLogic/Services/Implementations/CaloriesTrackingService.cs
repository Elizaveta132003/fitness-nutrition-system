using Calories.BusinessLogic.Dtos.RequestDtos;
using Calories.BusinessLogic.Dtos.ResponseDtos;
using Calories.BusinessLogic.Exceptions;
using Calories.BusinessLogic.Helpers;
using Calories.BusinessLogic.Services.Interfaces;
using Calories.DataAccess.Entities;
using Calories.DataAccess.Repositories.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Calories.BusinessLogic.Services.Implementations
{
    public class CaloriesTrackingService : ICaloriesTrackingService
    {
        private readonly ICaloriesTrackingRepository _caloriesTrackingRepository;
        private readonly ILogger<CaloriesTrackingService> _logger;

        public CaloriesTrackingService(ICaloriesTrackingRepository caloriesTrackingRepository, ILogger<CaloriesTrackingService> logger)
        {
            _caloriesTrackingRepository = caloriesTrackingRepository;
            _logger = logger;
        }

        public async Task<CaloriesTrackingResponseDto> CreateAsync(CaloriesTrackingRequestDto caloriesTracking,
            CancellationToken cancellationToken = default)
        {
            var createCaloriesTracking = caloriesTracking.Adapt<CaloriesTracking>();
            createCaloriesTracking.Date = createCaloriesTracking.Date.Date;

            await _caloriesTrackingRepository.CreateAsync(createCaloriesTracking, cancellationToken);

            _logger.LogInformation($"Calories tracking with id {createCaloriesTracking.Id} was successfully created");

            var caloriesTrackingResponseDto = createCaloriesTracking.Adapt<CaloriesTrackingResponseDto>();

            return caloriesTrackingResponseDto;
        }

        public async Task<CaloriesTrackingResponseDto> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var existingCaloriesTracking = await _caloriesTrackingRepository.GetByIdAsync(id, cancellationToken);

            if (existingCaloriesTracking is null)
            {
                _logger.LogError($"Calories tracking with id {id} not found");

                throw new NotFoundException(CaloriesTrackingErrorMessages.CaloriesTrackingNotFound);
            }

            await _caloriesTrackingRepository.DeleteAsync(id, cancellationToken);

            _logger.LogInformation($"Calories tracking with id {id} was successfully deleted");

            var caloriesTrackingResponseDto = existingCaloriesTracking.Adapt<CaloriesTrackingResponseDto>();

            return caloriesTrackingResponseDto;
        }

        public async Task<IEnumerable<CaloriesTrackingResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var caloriesTrackingCollection = await _caloriesTrackingRepository.GetAllAsync(cancellationToken);

            _logger.LogInformation($"Calories trackings were successfully received");

            var result = caloriesTrackingCollection.Adapt<IEnumerable<CaloriesTrackingResponseDto>>();

            return result;
        }

        public async Task<CaloriesTrackingResponseDto> UpdateAsync(string id, CaloriesTrackingRequestDto caloriesTracking, CancellationToken cancellationToken = default)
        {
            var existingCaloriesTracking = await _caloriesTrackingRepository.GetByIdAsync(id, cancellationToken);

            if (existingCaloriesTracking is null)
            {
                _logger.LogError($"Calories tracking with id {id} not found");

                throw new NotFoundException(CaloriesTrackingErrorMessages.CaloriesTrackingNotFound);
            }

            var updateCaloriesTracking = caloriesTracking.Adapt<CaloriesTracking>();
            updateCaloriesTracking.Id = id;

            await _caloriesTrackingRepository.UpdateAsync(updateCaloriesTracking, cancellationToken);

            _logger.LogInformation($"Calories tracking with id {updateCaloriesTracking.Id} was successfully updated");

            var caloriesTrackingResponseDto = updateCaloriesTracking.Adapt<CaloriesTrackingResponseDto>();

            return caloriesTrackingResponseDto;
        }
    }
}
