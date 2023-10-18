using Workouts.BusinessLogic.Dtos.RequestDtos;
using Workouts.BusinessLogic.Dtos.ResponseDtos;
using Workouts.DataAccess.Enums;

namespace Workouts.BusinessLogic.Services.Interfaces
{
    public interface IExerciseService
    {
        public Task<ExerciseResponseDto> CreateAsync(ExerciseRequestDto exercise, CancellationToken cancellationToken = default);
        public Task<ExerciseResponseDto> UpdateAsync(Guid id, ExerciseRequestDto exercise, CancellationToken cancellationToken = default);
        public Task<ExerciseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<ExerciseResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        public Task<ExerciseResponseDto> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        public Task<IEnumerable<ExerciseResponseDto>> GetByTypeAsync(ExerciseType type, CancellationToken cancellationToken = default);
    }
}
