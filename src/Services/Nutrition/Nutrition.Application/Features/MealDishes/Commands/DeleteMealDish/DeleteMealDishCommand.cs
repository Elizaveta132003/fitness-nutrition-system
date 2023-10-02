using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.MealDishes.Commands.DeleteMealDish
{
    public record DeleteMealDishCommand(Guid Id) : IRequest<MealDishResponseDto>;
}
