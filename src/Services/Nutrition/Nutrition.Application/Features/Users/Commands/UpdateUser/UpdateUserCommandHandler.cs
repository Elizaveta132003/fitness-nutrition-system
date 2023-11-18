using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUserRepository userRepository, ILogger<UpdateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<UserResponseDto> Handle(UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var foundUser = await _userRepository.GetOneByAsync(user => user.Username == request.UserRequestDto.Username,
                cancellationToken);

            if (foundUser is null)
            {
                _logger.LogError($"User {request.UserRequestDto.Username} not found");

                throw new NotFoundException(UserErrorMessages.UserNotFound);
            }

            var user = request.UserRequestDto.Adapt<User>();

            _userRepository.Update(user);

            await _userRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User {user.Username} was successfully updated");

            var userResponseDto = user.Adapt<UserResponseDto>();

            return userResponseDto;
        }
    }
}
