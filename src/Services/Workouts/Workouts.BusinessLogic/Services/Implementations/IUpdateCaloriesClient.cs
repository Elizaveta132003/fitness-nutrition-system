using Workouts.BusinessLogic.Dtos.RequestDtos;

namespace Workouts.BusinessLogic.Services.Implementations
{
    public interface IUpdateCaloriesClient
    {
        Task UpdateCaloriesAsync(WorkoutExerciseRequestDto workoutExerciseRequestDto, CancellationToken cancellationToken = default);
    }
}
