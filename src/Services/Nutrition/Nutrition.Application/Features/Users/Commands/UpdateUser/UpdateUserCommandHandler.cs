using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var isFoundUser = await _userRepository.UserExistsAsync(
                request.UserRequestDto.Username, cancellationToken);

            if (!isFoundUser)
            {
                throw new NotFoundException(UserErrorMessages.UserNotFound);
            }

            var user = request.UserRequestDto.Adapt<User>();

            await _userRepository.UpdateAsync(user, cancellationToken);

            await _userRepository.SaveChangesAsync(cancellationToken);

            var userResponseDto = user.Adapt<UserResponseDto>();

            return userResponseDto;
        }
    }
}
