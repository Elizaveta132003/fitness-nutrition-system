using Mapster;
using MediatR;
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

        public CreateUserCommandHandler(IUserRepository userRepository, IFoodDiaryRepository foodDiaryRepository)
        {
            _userRepository = userRepository;
            _foodDiaryRepository = foodDiaryRepository;
        }

        public async Task<UserResponseDto> Handle(CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var isFoundUser = await _userRepository.UserExistsAsync(request.UserRequestDto.Username,
                cancellationToken);

            if (isFoundUser)
            {
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

            var userResponseDto = user.Adapt<UserResponseDto>();

            return userResponseDto;
        }
    }
}
