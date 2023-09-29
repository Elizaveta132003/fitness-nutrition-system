using MediatR;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.Food.Commands.CreateFood
{
    public record CreateFoodCommand(FoodRequestDto FoodRequestDto):IRequest<FoodResponseDto>;
}
