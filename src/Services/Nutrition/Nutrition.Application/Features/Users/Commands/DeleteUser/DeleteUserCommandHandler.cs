using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserRepository userRepository, ILogger<DeleteUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<UserResponseDto> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            var foundUser = await _userRepository.GetOneByAsync(user => user.Id == request.Id,
                cancellationToken);

            if (foundUser is null)
            {
                _logger.LogInformation($"User with id {request.Id} not found");

                throw new NotFoundException(UserErrorMessages.UserNotFound);
            }

            _userRepository.Delete(foundUser);

            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User {foundUser.Username} was successfully deleted");

            var userResponseDto = foundUser.Adapt<UserResponseDto>();

            return userResponseDto;
        }
    }
}
