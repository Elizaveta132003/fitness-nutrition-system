using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;

namespace Nutrition.Application.Features.Food.Queries.GetByNameFood
{
    public record GetByNameFoodQuery(string Name) : IRequest<FoodResponseDto>;
}
