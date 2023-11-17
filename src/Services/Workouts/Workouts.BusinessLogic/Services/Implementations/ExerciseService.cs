using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Workouts.BusinessLogic.Dtos.RequestDtos;
using Workouts.BusinessLogic.Dtos.ResponseDtos;
using Workouts.BusinessLogic.Exceptions;
using Workouts.BusinessLogic.Extensions;
using Workouts.BusinessLogic.Helpers;
using Workouts.BusinessLogic.Options;
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
        private readonly IDistributedCache _cache;
        private readonly IOptions<RedisCacheOptions> _cacheOptions;

        public ExerciseService(IExerciseRepository exerciseRepository, ILogger<ExerciseService> logger,IDistributedCache cache,
            IOptions<RedisCacheOptions> options)
        {
            _exerciseRepository = exerciseRepository;
            _logger = logger;
            _cache = cache;
            _cacheOptions = options;
        }

        public async Task<ExerciseResponseDto> CreateAsync(ExerciseRequestDto exercise,
            CancellationToken cancellationToken = default)
        {
            var foundExercise = await _exerciseRepository.GetOneByAsync(ex =>
            ex.Name == exercise.Name, cancellationToken);

            if(foundExercise is not null)
            {
                _logger.LogError("Exercise creation failed");

                throw new AlreadyExistsException(ExerciseErrorMessages.ExerciseAlreadyExists);
            }

            var createExercise = exercise.Adapt<Exercise>();
            createExercise.Id = Guid.NewGuid();

            _exerciseRepository.Create(createExercise);

            await _exerciseRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Exercise with id {createExercise.Id} created successfully");

            await _cache.RemoveAsync(CacheHelper.GetCacheKeyForAllExercises(), cancellationToken);

            var exerciseResponseDto = createExercise.Adapt<ExerciseResponseDto>();

            return exerciseResponseDto;
        }

        public async Task<ExerciseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Exercise foundExercise;
            var cacheKey = CacheHelper.GetCacheKeyForExercise(id);
            var isExerciseInCache = _cache.TryGetValue(cacheKey, out foundExercise);

            if(!isExerciseInCache)
            {
                foundExercise = await _exerciseRepository.GetOneByAsync(exercise => exercise.Id == id, cancellationToken);
            }

            if(foundExercise is null)
            {
                _logger.LogError($"Exercise with id {id} not found;");

                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            _exerciseRepository.Delete(foundExercise);

            await _exerciseRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Exercise {foundExercise.Name} was successfully deleted");

            await _cache.RemoveAsync(CacheHelper.GetCacheKeyForAllExercises(), cancellationToken);

            var exerciseResponseDto = foundExercise.Adapt<ExerciseResponseDto>();

            if(isExerciseInCache)
            {
                await _cache.RemoveAsync(cacheKey, cancellationToken);
            }

            return exerciseResponseDto;
        }

        public async Task<IEnumerable<ExerciseResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<Exercise> exercises;
            var cacheKey = CacheHelper.GetCacheKeyForAllExercises();
            var isExercisesInCache = _cache.TryGetValue(cacheKey, out exercises);

            if(!isExercisesInCache)
            {
                exercises = await _exerciseRepository.GetAllExercisesAsync(cancellationToken);
            }

            if(exercises is not null)
            {
                await exercises.AddListToCacheAsync(_cache, _cacheOptions);
            }

            _logger.LogInformation("Exercises were successfully received");

            var result = exercises.Adapt<IEnumerable<ExerciseResponseDto>>();

            return result;
        }

        public async Task<ExerciseResponseDto> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var existingExercise = await _exerciseRepository.GetOneByAsync(exercise => exercise.Name == name, cancellationToken);

            if(existingExercise is null)
            {
                _logger.LogError($"Exercise {name} not found");

                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            _logger.LogInformation($"Exercise {name} was successfully received");

            var result = existingExercise.Adapt<ExerciseResponseDto>();

            return result;
        }

        public async Task<IEnumerable<ExerciseResponseDto>> GetByTypeAsync(ExerciseType type, CancellationToken cancellationToken = default)
        {
            var existingExercise = await _exerciseRepository.GetAllByAsync(exercise => exercise.ExerciseType == type, cancellationToken);

            if(existingExercise is null)
            {
                _logger.LogError($"Exercises with type {type} not found");

                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            _logger.LogInformation($"Exercises by type {type} were successfully received");

            var result = existingExercise.Adapt<IEnumerable<ExerciseResponseDto>>();

            return result;
        }

        public async Task<ExerciseResponseDto> UpdateAsync(Guid id, ExerciseRequestDto exercise,
            CancellationToken cancellationToken = default)
        {
            Exercise foundExercise;
            var cacheKey = CacheHelper.GetCacheKeyForExercise(id);
            var isExerciseInCache = _cache.TryGetValue(cacheKey, out foundExercise);

            if(!isExerciseInCache)
            {
                foundExercise = await _exerciseRepository.GetOneByAsync(exercise => exercise.Id == id, cancellationToken);
            }

            if(foundExercise is null)
            {
                _logger.LogError($"Exercise with id {id} not found");

                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            var updateExercise = exercise.Adapt<Exercise>();
            updateExercise.Id = id;

            _exerciseRepository.Update(updateExercise);

            var exerciseResponseDto = updateExercise.Adapt<ExerciseResponseDto>();

            await _exerciseRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Exercise {updateExercise.Name} was successfully updated");

            var exerciseResponseDto = updateExercise.Adapt<ExerciseResponseDto>();

            await _cache.RemoveAsync(CacheHelper.GetCacheKeyForAllExercises(), cancellationToken);

            if(!isExerciseInCache)
            {
                await updateExercise.AddToCacheAsync(_cache, _cacheOptions);
            }

            return exerciseResponseDto;
        }
    }
}
