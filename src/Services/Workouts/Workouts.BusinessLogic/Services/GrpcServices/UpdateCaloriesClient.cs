using Google.Protobuf.WellKnownTypes;
using Mapster;
using Microsoft.Extensions.Logging;
using Workouts.BusinessLogic.Dtos.RequestDtos;
using Workouts.BusinessLogic.Dtos.ResponseDtos;
using Workouts.BusinessLogic.Exceptions;
using Workouts.BusinessLogic.Helpers;
using Workouts.BusinessLogic.Protos;
using Workouts.BusinessLogic.Services.Implementations;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.BusinessLogic.Services.GrpcServices
{
    public class UpdateCaloriesClient : IUpdateCaloriesClient
    {
        private readonly CaloriesService.CaloriesServiceClient _caloriesServiceClient;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ILogger<UpdateCaloriesClient> _logger;

        public UpdateCaloriesClient(CaloriesService.CaloriesServiceClient caloriesServiceClient,
            IExerciseRepository exerciseRepository,
            ILogger<UpdateCaloriesClient> logger)
        {
            _caloriesServiceClient = caloriesServiceClient;
            _exerciseRepository = exerciseRepository;
            _logger = logger;
        }

        public async Task UpdateCaloriesAsync(WorkoutExerciseRequestDto workoutExerciseRequestDto,
            CancellationToken cancellationToken = default)
        {
            var exercise = await GetExerciseByIdAsync(workoutExerciseRequestDto.ExerciseId);

            var request = new UpdateCaloriesRequest
            {
                Calories = workoutExerciseRequestDto.Duration * exercise.CaloriesBurnedPerMinute,
                UserId = workoutExerciseRequestDto.Workout.ExercisesDiary.UserId.ToString(),
                Date = workoutExerciseRequestDto.Workout.Date.ToTimestamp(),
                IsCaloriesConsumed = false
            };

            var reply = await _caloriesServiceClient.UpdateCaloriesAsync(request);

            _logger.LogInformation("Calories updated successfully");
        }

        private async Task<ExerciseResponseDto> GetExerciseByIdAsync(Guid id)
        {
            var existingExercise = await _exerciseRepository.GetOneByAsync(exercise => exercise.Id == id);

            if (existingExercise is null)
            {
                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            var responseModel = existingExercise.Adapt<ExerciseResponseDto>();

            return responseModel;
        }
    }
}
