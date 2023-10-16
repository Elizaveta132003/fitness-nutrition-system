using Calories.BusinessLogic.Dtos.RequestDtos;
using Calories.BusinessLogic.Dtos.ResponseDtos;
using Calories.BusinessLogic.Exceptions;
using Calories.BusinessLogic.Helpers;
using Calories.BusinessLogic.Services.Interfaces;
using Calories.DataAccess.Entities;
using Calories.DataAccess.Repositories.Interfaces;
using Mapster;

namespace Calories.BusinessLogic.Services.Implementations
{
    public class CaloriesTrackingService : ICaloriesTrackingService
    {
        private readonly ICaloriesTrackingRepository _caloriesTrackingRepository;

        public CaloriesTrackingService(ICaloriesTrackingRepository caloriesTrackingRepository)
        {
            _caloriesTrackingRepository = caloriesTrackingRepository;
        }

        public async Task<CaloriesTrackingResponseDto> CreateAsync(CaloriesTrackingRequestDto caloriesTracking,
            CancellationToken cancellationToken = default)
        {
            var createCaloriesTracking = caloriesTracking.Adapt<CaloriesTracking>();

            await _caloriesTrackingRepository.CreateAsync(createCaloriesTracking, cancellationToken);

            var caloriesTrackingResponseDto = createCaloriesTracking.Adapt<CaloriesTrackingResponseDto>();

            return caloriesTrackingResponseDto;
        }

        public async Task<CaloriesTrackingResponseDto> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var existingCaloriesTracking = await _caloriesTrackingRepository.GetByIdAsync(id, cancellationToken);

            if (existingCaloriesTracking is null)
            {
                throw new NotFoundException(CaloriesTrackingErrorMessages.CaloriesTrackingNotFound);
            }

            await _caloriesTrackingRepository.DeleteAsync(id, cancellationToken);

            var caloriesTrackingResponseDto = existingCaloriesTracking.Adapt<CaloriesTrackingResponseDto>();

            return caloriesTrackingResponseDto;
        }

        public async Task<IEnumerable<CaloriesTrackingResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var caloriesTrackingCollection = await _caloriesTrackingRepository.GetAllAsync(cancellationToken);

            var result = caloriesTrackingCollection.Adapt<IEnumerable<CaloriesTrackingResponseDto>>();

            return result;
        }

        public async Task<CaloriesTrackingResponseDto> UpdateAsync(string id, CaloriesTrackingRequestDto caloriesTracking, CancellationToken cancellationToken = default)
        {
            var existingCaloriesTracking = await _caloriesTrackingRepository.GetByIdAsync(id, cancellationToken);

            if (existingCaloriesTracking is null)
            {
                throw new NotFoundException(CaloriesTrackingErrorMessages.CaloriesTrackingNotFound);
            }

            var updateCaloriesTracking = caloriesTracking.Adapt<CaloriesTracking>();
            updateCaloriesTracking.Id = id;

            await _caloriesTrackingRepository.UpdateAsync(updateCaloriesTracking, cancellationToken);

            var caloriesTrackingResponseDto = updateCaloriesTracking.Adapt<CaloriesTrackingResponseDto>();

            return caloriesTrackingResponseDto;
        }
    }
}
