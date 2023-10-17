using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.Food.Queries.GetAllFood
{
    public record GetAllFoodQuery() : IRequest<IEnumerable<FoodResponseDto>>;
}
