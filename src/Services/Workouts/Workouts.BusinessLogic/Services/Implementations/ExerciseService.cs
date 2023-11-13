using Mapster;
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
        private readonly IDistributedCache _cache;
        private readonly IOptions<RedisCacheOptions> _cacheOptions;

        public ExerciseService(IExerciseRepository exerciseRepository, IDistributedCache cache,
            IOptions<RedisCacheOptions> options)
        {
            _exerciseRepository = exerciseRepository;
            _cache = cache;
            _cacheOptions = options;
        }

        public async Task<ExerciseResponseDto> CreateAsync(ExerciseRequestDto exercise,
            CancellationToken cancellationToken = default)
        {
            var foundExercise = await _exerciseRepository.GetOneByAsync(ex =>
            ex.Name == exercise.Name, cancellationToken);

            if (foundExercise is not null)
            {
                throw new AlreadyExistsException(ExerciseErrorMessages.ExerciseAlreadyExists);
            }

            var createExercise = exercise.Adapt<Exercise>();
            createExercise.Id = Guid.NewGuid();

            _exerciseRepository.Create(createExercise);

            await _exerciseRepository.SaveChangesAsync(cancellationToken);

            var exerciseResponseDto = createExercise.Adapt<ExerciseResponseDto>();

            return exerciseResponseDto;
        }

        public async Task<ExerciseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Exercise foundExercise;
            var cacheKey = CacheHelper.GetCacheKeyForExercise(id);
            var isExerciseInCache = _cache.TryGetValue(cacheKey, out foundExercise);

            if (!isExerciseInCache)
            {
                foundExercise = await _exerciseRepository.GetOneByAsync(exercise => exercise.Id == id, cancellationToken);
            }

            if (foundExercise is null)
            {
                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            _exerciseRepository.Delete(foundExercise);

            await _exerciseRepository.SaveChangesAsync(cancellationToken);

            var exerciseResponseDto = foundExercise.Adapt<ExerciseResponseDto>();

            if (!isExerciseInCache)
            {
                return exerciseResponseDto;
            }

            await _cache.RemoveAsync(cacheKey, cancellationToken);

            return exerciseResponseDto;
        }

        public async Task<IEnumerable<ExerciseResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var exercises = await _exerciseRepository.GetAllExercisesAsync(cancellationToken);

            var result = exercises.Adapt<IEnumerable<ExerciseResponseDto>>();

            return result;
        }

        public async Task<ExerciseResponseDto> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var existingExercise = await _exerciseRepository.GetOneByAsync(exercise => exercise.Name == name, cancellationToken);

            if (existingExercise is null)
            {
                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            var result = existingExercise.Adapt<ExerciseResponseDto>();

            return result;
        }

        public async Task<IEnumerable<ExerciseResponseDto>> GetByTypeAsync(ExerciseType type, CancellationToken cancellationToken = default)
        {
            var existingExercise = await _exerciseRepository.GetAllByAsync(exercise => exercise.ExerciseType == type, cancellationToken);

            if (existingExercise is null)
            {
                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            var result = existingExercise.Adapt<IEnumerable<ExerciseResponseDto>>();

            return result;
        }

        public async Task<ExerciseResponseDto> UpdateAsync(Guid id, ExerciseRequestDto exercise,
            CancellationToken cancellationToken = default)
        {
            Exercise foundExercise;
            var cacheKey = CacheHelper.GetCacheKeyForExercise(id);
            var isExerciseInCache = _cache.TryGetValue(cacheKey, out foundExercise);

            if (!isExerciseInCache)
            {
                foundExercise = await _exerciseRepository.GetOneByAsync(exercise => exercise.Id == id, cancellationToken);
            }

            if (foundExercise is null)
            {
                throw new NotFoundException(ExerciseErrorMessages.ExerciseNotFound);
            }

            var updateExercise = exercise.Adapt<Exercise>();
            updateExercise.Id = id;

            _exerciseRepository.Update(updateExercise);

            var exerciseResponseDto = updateExercise.Adapt<ExerciseResponseDto>();

            await _exerciseRepository.SaveChangesAsync(cancellationToken);

            if (isExerciseInCache)
            {
                return exerciseResponseDto;
            }

            await updateExercise.AddToCacheAsync(_cache, _cacheOptions);

            return exerciseResponseDto;
        }
    }
}
