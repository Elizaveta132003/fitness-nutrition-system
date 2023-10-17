using MediatR;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(UserRequestDto UserRequestDto):IRequest<UserResponseDto>;
}
