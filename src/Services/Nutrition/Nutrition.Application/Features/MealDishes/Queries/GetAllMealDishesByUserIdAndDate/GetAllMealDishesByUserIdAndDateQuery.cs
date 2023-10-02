using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.MealDishes.Queries.GetAllMealDishesByUserIdAndDate
{
    public record GetAllMealDishesByUserIdAndDateQuery(Guid UserId, DateTime Date) :
        IRequest<IEnumerable<MealDishResponseDto>>;
}
