using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.MealDishes.Queries.GetAllMealDishesByUserIdAndDate
{
    public class GetAllMealDishesByUserIdAndDateQueryHandler :
        IRequestHandler<GetAllMealDishesByUserIdAndDateQuery, IEnumerable<MealDishResponseDto>>
    {
        private readonly IMealDishRepository _mealDishRepository;

        public GetAllMealDishesByUserIdAndDateQueryHandler(IMealDishRepository mealDishRepository)
        {
            _mealDishRepository = mealDishRepository;
        }

        public async Task<IEnumerable<MealDishResponseDto>> Handle
            (GetAllMealDishesByUserIdAndDateQuery request, CancellationToken cancellationToken)
        {
            var foundMealDishes = await _mealDishRepository.GetAllMealDishesByUserIdAndDateAsync(
                request.UserId, request.Date, cancellationToken);

            if (!foundMealDishes.Any())
            {
                throw new NotFoundException(MealDishErrorMessages.NoData);
            }

            var responseModel = foundMealDishes.Adapt<IEnumerable<MealDishResponseDto>>();

            return responseModel;
        }
    }
}
