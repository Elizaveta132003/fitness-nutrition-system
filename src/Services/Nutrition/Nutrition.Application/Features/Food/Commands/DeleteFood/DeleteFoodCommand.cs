using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.Food.Commands.DeleteFood
{
    public record DeleteFoodCommand(Guid Id) : IRequest<FoodResponseDto>;
}
