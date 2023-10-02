using MediatR;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.MealDishes.Commands.CreateMealDish
{
    public record CreateMealDishCommand(MealDishRequestDto MealDishRequestDto) : IRequest<MealDishResponseDto>;
}
