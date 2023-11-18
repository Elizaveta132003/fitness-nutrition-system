using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFoodDiaryRepository _foodDiaryRepository;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository,
            IFoodDiaryRepository foodDiaryRepository,
            ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _foodDiaryRepository = foodDiaryRepository;
            _logger = logger;
        }

        public async Task<UserResponseDto> Handle(CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var foundUser = await _userRepository.GetOneByAsync(user => user.Username == request.UserRequestDto.Username,
                cancellationToken);

            if (foundUser is not null)
            {
                _logger.LogError($"User {request.UserRequestDto.Username} not found");

                throw new AlreadyExistsException(UserErrorMessages.UserAlreadyExists);
            }

            var user = request.UserRequestDto.Adapt<User>();
            user.Id = Guid.NewGuid();

            _userRepository.Create(user);

            var foodDiary = new FoodDiary()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id
            };

            _foodDiaryRepository.Create(foodDiary);

            await _foodDiaryRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User {user.Username} and food diry with id {foodDiary.Id} were successfully created");

            var userResponseDto = user.Adapt<UserResponseDto>();

            return userResponseDto;
        }
    }
}
