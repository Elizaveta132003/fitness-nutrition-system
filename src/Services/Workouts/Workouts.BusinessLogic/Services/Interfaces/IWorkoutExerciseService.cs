using Workouts.BusinessLogic.Dtos.RequestDtos;
using Workouts.BusinessLogic.Dtos.ResponseDtos;
using Workouts.DataAccess.Enums;

namespace Workouts.BusinessLogic.Services.Interfaces
{
    public interface IWorkoutExerciseService
    {
        public Task<WorkoutExerciseResponseDto> CreateAsync(WorkoutExerciseRequestDto workoutExercise, CancellationToken cancellationToken = default);
        public Task<WorkoutExerciseResponseDto> UpdateAsync(Guid id, WorkoutExerciseRequestDto workoutExercise, CancellationToken cancellationToken = default);
        public Task<WorkoutExerciseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<IEnumerable<WorkoutExerciseResponseDto>> GetAllWorkoutExercisesByUserIdAndDateAsync(
            Guid userId, DateTime date, CancellationToken cancellationToken = default);
        public Task<IEnumerable<WorkoutExerciseResponseDto>> GetAllWorkoutExercisesByUserIdAndDateAndTypeAsync(
             Guid userId, DateTime date, ExerciseType type, CancellationToken cancellationToken = default);
    }
}
