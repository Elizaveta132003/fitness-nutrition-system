using MediatR;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.Food.Commands.UpdateFood
{
    public record UpdateFoodCommand(Guid Id, FoodRequestDto FoodRequestDto) : IRequest<FoodResponseDto>;
}
