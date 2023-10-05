using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            var foundUser = await _userRepository.GetByIdAsync(request.id, cancellationToken);

            if (foundUser is null)
            {
                throw new NotFoundException(UserErrorMessages.UserNotFound);
            }

            _userRepository.Delete(foundUser);

            await _userRepository.SaveChangesAsync(cancellationToken);

            var userResponseDto = foundUser.Adapt<UserResponseDto>();

            return userResponseDto;
        }
    }
}
