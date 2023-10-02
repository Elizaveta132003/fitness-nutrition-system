using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Domain.Enums;

namespace Nutrition.Application.Features.MealDishes.Queries.GetAllMealDishesByUserIdAndDateAndMealType
{
    public record GetAllMealDishesByUserIdAndDateAndMealTypeQuery(Guid UserId, DateTime Date, MealType MealType) :
        IRequest<IEnumerable<MealDishResponseDto>>;
}
