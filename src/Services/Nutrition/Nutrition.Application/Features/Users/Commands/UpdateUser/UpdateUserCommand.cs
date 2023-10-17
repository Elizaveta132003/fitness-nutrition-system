using MediatR;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(UserRequestDto UserRequestDto) : IRequest<UserResponseDto>;
}
