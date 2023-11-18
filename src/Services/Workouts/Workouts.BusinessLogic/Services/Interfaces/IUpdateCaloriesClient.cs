using Workouts.BusinessLogic.Dtos.RequestDtos;

namespace Workouts.BusinessLogic.Services.Interfaces
{
    public interface IUpdateCaloriesClient
    {
        Task UpdateCaloriesAsync(WorkoutExerciseRequestDto workoutExerciseRequestDto, CancellationToken cancellationToken = default);
    }
}
