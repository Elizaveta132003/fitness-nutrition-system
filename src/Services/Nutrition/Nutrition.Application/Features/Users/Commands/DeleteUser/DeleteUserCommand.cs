using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid Id) : IRequest<UserResponseDto>;
}
