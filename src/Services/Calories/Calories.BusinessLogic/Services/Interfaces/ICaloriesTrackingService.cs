using Calories.BusinessLogic.Dtos.RequestDtos;
using Calories.BusinessLogic.Dtos.ResponseDtos;

namespace Calories.BusinessLogic.Services.Interfaces
{
    public interface ICaloriesTrackingService
    {
        public Task<CaloriesTrackingResponseDto> CreateAsync(CaloriesTrackingRequestDto caloriesTracking, CancellationToken cancellationToken = default);
        public Task<CaloriesTrackingResponseDto> UpdateAsync(string id, CaloriesTrackingRequestDto caloriesTracking, CancellationToken cancellationToken = default);
        public Task<CaloriesTrackingResponseDto> DeleteAsync(string id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<CaloriesTrackingResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
