using Mapster;
using Microsoft.Extensions.Logging;
using Workouts.BusinessLogic.Dtos.RequestDtos;
using Workouts.BusinessLogic.Dtos.ResponseDtos;
using Workouts.BusinessLogic.Exceptions;
using Workouts.BusinessLogic.Helpers;
using Workouts.BusinessLogic.Services.Interfaces;
using Workouts.DataAccess.Entities;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IExercisesDiaryRepository _exercisesDiaryRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository,
            IExercisesDiaryRepository exercisesDiaryRepository,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _exercisesDiaryRepository = exercisesDiaryRepository;
            _logger = logger;
        }

        public async Task<UserResponseDto> CreateAsync(UserRequestDto user, CancellationToken cancellationToken = default)
        {
            var foundUser = await _userRepository.GetOneByAsync(u => u.Username == user.Username, cancellationToken);

            if (foundUser is not null)
            {
                _logger.LogError($"User {foundUser.Username} already exists");

                throw new AlreadyExistsException(UserErrorMessages.UserAlreadyExists);
            }

            var createUser = user.Adapt<User>();
            createUser.Id = Guid.NewGuid();

            _userRepository.Create(createUser);

            var exercisesDiary = new ExercisesDiary()
            {
                Id = Guid.NewGuid(),
                UserId = createUser.Id
            };

            _exercisesDiaryRepository.Create(exercisesDiary);

            await _exercisesDiaryRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User {createUser.Username} and diary with id {exercisesDiary.Id} were successfully created");

            var userResponseDto = createUser.Adapt<UserResponseDto>();

            return userResponseDto;
        }

        public async Task<UserResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var foundUser = await _userRepository.GetOneByAsync(user => user.Id == id, cancellationToken);

            if (foundUser is null)
            {
                _logger.LogError($"User with id {id} not found");

                throw new NotFoundException(UserErrorMessages.UserNotFound);
            }

            _userRepository.Delete(foundUser);

            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User {foundUser.Username} was successfully deleted");

            var userResponseDto = foundUser.Adapt<UserResponseDto>();

            return userResponseDto;
        }

        public async Task<UserResponseDto> UpdateAsync(Guid id, UserRequestDto user, CancellationToken cancellationToken = default)
        {
            var foundUser = await _userRepository.GetOneByAsync(u => u.Username == user.Username, cancellationToken);

            if (foundUser is null)
            {
                _logger.LogError($"User with id {id} not found");

                throw new NotFoundException(UserErrorMessages.UserNotFound);
            }

            var updateUser = user.Adapt<User>();

            _userRepository.Update(updateUser);

            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User {updateUser.Username} was successfully updated");

            var userResponseDto = updateUser.Adapt<UserResponseDto>();

            return userResponseDto;
        }
    }
}
