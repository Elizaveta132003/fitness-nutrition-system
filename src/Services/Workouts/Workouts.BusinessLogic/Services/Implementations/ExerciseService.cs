using Mapster;
using Microsoft.Extensions.Logging;
using Workouts.BusinessLogic.Dtos.RequestDtos;
using Workouts.BusinessLogic.Dtos.ResponseDtos;
using Workouts.BusinessLogic.Exceptions;
using Workouts.BusinessLogic.Helpers;
using Workouts.BusinessLogic.Services.Interfaces;
using Workouts.DataAccess.Entities;
using Workouts.DataAccess.Enums;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.BusinessLogic.Services.Implementations
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ILogger<ExerciseService> _logger;

        public ExerciseService(IExerciseRepository exerciseRepository, ILogger<ExerciseService> logger)
        {
            _exerciseRepository = exerciseRepository;
            _logger = logger;
        }

        public async Task<ExerciseResponseDto> CreateAsync(ExerciseRequestDto exercise,
            CancellationToken cancellationToken = default)
        {
            var foundExercise = await _exerciseRepository.GetOneByAsync(ex =>
            ex.Name == exercise.Name, cancellationToken);

            if (foundExercise is not null)
            {
                _logger.LogInformation("Exercise creation failed");

                throw new AlreadyExistsException(ExerciseErrorMessages.ExerciseAlreadyExists);
            }

            var createExercise = exercise.Adapt<Exercise>();
            createExercise.Id = Guid.NewGuid();

            _exerciseRepository.Create(createExercise);

            await _exerciseRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Exercise with id {createExercise.Id} created successfully");

            var exerciseResponseDto = createExercise.Adapt<ExerciseResponseDto>();

            return exerciseResponseDto;
        }

        public async Task<ExerciseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var foundExercise = await _exerciseRepository.GetOneByAsync(exercise => exercise.Id == id, cancellationToken);

            if (foundExercise is null)
            {
                _logger.LogInformation($"Exercise with id {id} not found;");

                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            _exerciseRepository.Delete(foundExercise);

            await _exerciseRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Exercise {foundExercise.Name} was successfully deleted");

            var exerciseResponseDto = foundExercise.Adapt<ExerciseResponseDto>();

            return exerciseResponseDto;
        }

        public async Task<IEnumerable<ExerciseResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var exercises = await _exerciseRepository.GetAllExercisesAsync(cancellationToken);

            _logger.LogInformation("Exercises were successfully received");

            var result = exercises.Adapt<IEnumerable<ExerciseResponseDto>>();

            return result;
        }

        public async Task<ExerciseResponseDto> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var existingExercise = await _exerciseRepository.GetOneByAsync(exercise => exercise.Name == name, cancellationToken);

            if (existingExercise is null)
            {
                _logger.LogInformation($"Exercise {name} not found");

                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            _logger.LogInformation($"Exercise {name} was successfully received");

            var result = existingExercise.Adapt<ExerciseResponseDto>();

            return result;
        }

        public async Task<IEnumerable<ExerciseResponseDto>> GetByTypeAsync(ExerciseType type, CancellationToken cancellationToken = default)
        {
            var existingExercise = await _exerciseRepository.GetAllByAsync(exercise => exercise.ExerciseType == type, cancellationToken);

            if (existingExercise is null)
            {
                _logger.LogInformation($"Exercises with type {type} not found");

                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            _logger.LogInformation($"Exercises by type {type} were successfully received");

            var result = existingExercise.Adapt<IEnumerable<ExerciseResponseDto>>();

            return result;
        }

        public async Task<ExerciseResponseDto> UpdateAsync(Guid id, ExerciseRequestDto exercise,
            CancellationToken cancellationToken = default)
        {
            var foundExercise = await _exerciseRepository.GetOneByAsync(exercise => exercise.Id == id, cancellationToken);

            if (foundExercise is null)
            {
                _logger.LogInformation($"Exercise with id {id} not found");

                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            var updateExercise = exercise.Adapt<Exercise>();
            updateExercise.Id = id;

            _exerciseRepository.Update(updateExercise);

            await _exerciseRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Exercise {updateExercise.Name} was successfully updated");

            var exerciseResponseDto = updateExercise.Adapt<ExerciseResponseDto>();

            return exerciseResponseDto;
        }
    }
}
