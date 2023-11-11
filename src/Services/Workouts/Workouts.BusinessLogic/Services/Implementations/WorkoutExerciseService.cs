using Mapster;
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
    public class WorkoutExerciseService : IWorkoutExerciseService
    {
        private readonly IWorkoutExerciseRepository _workoutExerciseRepository;
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IExercisesDiaryRepository _exercisesDiaryRepository;
        private readonly IUpdateCaloriesClient _updateCaloriesClient;

        public WorkoutExerciseService(IWorkoutExerciseRepository workoutExerciseRepository,
            IWorkoutRepository workoutRepository,
            IExercisesDiaryRepository exercisesDiaryRepository,
            IUpdateCaloriesClient updateCaloriesClient)
        {
            _workoutExerciseRepository = workoutExerciseRepository;
            _workoutRepository = workoutRepository;
            _exercisesDiaryRepository = exercisesDiaryRepository;
            _updateCaloriesClient = updateCaloriesClient;
        }

        public async Task<WorkoutExerciseResponseDto> CreateAsync(WorkoutExerciseRequestDto workoutExercise,
            CancellationToken cancellationToken = default)
        {
            var createWorkoutExercise = workoutExercise.Adapt<WorkoutExercise>();
            createWorkoutExercise.Id = Guid.NewGuid();

            var userId = workoutExercise.Workout.ExercisesDiary.UserId;
            var workout = await GetWorkoutAsync(createWorkoutExercise, userId, cancellationToken);

            createWorkoutExercise.WorkoutId = workout.Id;
            createWorkoutExercise.Workout = null;

            _workoutExerciseRepository.Create(createWorkoutExercise);

            await _workoutExerciseRepository.SaveChangesAsync(cancellationToken);

            await _updateCaloriesClient.UpdateCaloriesAsync(workoutExercise, cancellationToken);

            var workoutExerciseResponseDto = createWorkoutExercise.Adapt<WorkoutExerciseResponseDto>();

            return workoutExerciseResponseDto;
        }

        public async Task<WorkoutExerciseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var foundWorkoutExercise = await _workoutExerciseRepository.GetOneByAsync(
                workoutExercise => workoutExercise.Id == id,
                cancellationToken);

            if (foundWorkoutExercise is null)
            {
                throw new NotFoundException(WorkoutExerciseErrorMessages.WorkoutExerciseNotFound);
            }

            _workoutExerciseRepository.Delete(foundWorkoutExercise);

            await _workoutExerciseRepository.SaveChangesAsync(cancellationToken);

            var workoutExerciseResponseDto = foundWorkoutExercise.Adapt<WorkoutExerciseResponseDto>();

            return workoutExerciseResponseDto;
        }

        public async Task<IEnumerable<WorkoutExerciseResponseDto>> GetAllWorkoutExercisesByUserIdAndDateAndTypeAsync(Guid userId,
            DateTime date, ExerciseType type, CancellationToken cancellationToken = default)
        {
            var foundWorkoutExercise = await _workoutExerciseRepository.GetAllByAsync(workoutExercise =>
            workoutExercise.Workout.ExercisesDiary.UserId == userId &&
            workoutExercise.Workout.Date == date &&
            workoutExercise.Exercise.ExerciseType == type, cancellationToken);

            if (!foundWorkoutExercise.Any())
            {
                throw new NotFoundException(WorkoutExerciseErrorMessages.WorkoutExerciseNotFound);
            }

            var result = foundWorkoutExercise.Adapt<IEnumerable<WorkoutExerciseResponseDto>>();

            return result;
        }

        public async Task<IEnumerable<WorkoutExerciseResponseDto>> GetAllWorkoutExercisesByUserIdAndDateAsync(Guid userId,
            DateTime date, CancellationToken cancellationToken = default)
        {
            var foundWorkoutExercise = await _workoutExerciseRepository.GetAllByAsync(workoutExercise =>
            workoutExercise.Workout.ExercisesDiary.UserId == userId &&
            workoutExercise.Workout.Date == date, cancellationToken);

            if (!foundWorkoutExercise.Any())
            {
                throw new NotFoundException(WorkoutExerciseErrorMessages.WorkoutExerciseNotFound);
            }

            var result = foundWorkoutExercise.Adapt<IEnumerable<WorkoutExerciseResponseDto>>();

            return result;
        }

        public async Task<WorkoutExerciseResponseDto> UpdateAsync(Guid id, WorkoutExerciseRequestDto workoutExercise,
            CancellationToken cancellationToken = default)
        {
            var foundWorkoutExercise = await _workoutExerciseRepository.GetOneByAsync(workoutExercise =>
            workoutExercise.Id == id, cancellationToken);

            if (foundWorkoutExercise is null)
            {
                throw new NotFoundException(WorkoutExerciseErrorMessages.WorkoutExerciseNotFound);
            }

            _workoutExerciseRepository.Update(foundWorkoutExercise);

            await _workoutExerciseRepository.SaveChangesAsync(cancellationToken);

            var workoutExerciseResponseDto = foundWorkoutExercise.Adapt<WorkoutExerciseResponseDto>();

            return workoutExerciseResponseDto;
        }
        private async Task<Workout> GetWorkoutAsync(WorkoutExercise workoutExercise, Guid userId, CancellationToken cancellationToken)
        {
            var foundWorkout = await _workoutRepository.GetOneByAsync(workout => workout.Date == workoutExercise.Workout.Date,
                cancellationToken);

            if (foundWorkout is null)
            {
                return await CreateWorkoutAsync(workoutExercise, userId, cancellationToken);
            }

            return foundWorkout;
        }

        private async Task<Workout> CreateWorkoutAsync(WorkoutExercise workoutExercise, Guid userId,
            CancellationToken cancellationToken)
        {
            var exerciseDiary = await _exercisesDiaryRepository.GetOneByAsync(exerciseDiary => exerciseDiary.UserId == userId,
                cancellationToken);

            var workout = new Workout()
            {
                Id = Guid.NewGuid(),
                ExercisesDiaryId = exerciseDiary.Id,
                Date = workoutExercise.Workout.Date
            };

            _workoutRepository.Create(workout);

            return workout;
        }
    }
}
