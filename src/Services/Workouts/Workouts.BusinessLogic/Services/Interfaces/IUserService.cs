using Workouts.BusinessLogic.Dtos.RequestDtos;
using Workouts.BusinessLogic.Dtos.ResponseDtos;

namespace Workouts.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponseDto> CreateAsync(UserRequestDto user, CancellationToken cancellationToken = default);
        public Task<UserResponseDto> UpdateAsync(Guid id, UserRequestDto user, CancellationToken cancellationToken = default);
        public Task<UserResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
