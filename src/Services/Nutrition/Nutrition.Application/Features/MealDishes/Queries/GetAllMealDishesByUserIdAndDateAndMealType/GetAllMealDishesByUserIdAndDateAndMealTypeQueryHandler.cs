using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.MealDishes.Queries.GetAllMealDishesByUserIdAndDateAndMealType
{
    public class GetAllMealDishesByUserIdAndDateAndMealTypeQueryHandler :
        IRequestHandler<GetAllMealDishesByUserIdAndDateAndMealTypeQuery, IEnumerable<MealDishResponseDto>>
    {
        private readonly IMealDishRepository _mealDishRepository;

        public GetAllMealDishesByUserIdAndDateAndMealTypeQueryHandler(IMealDishRepository mealDishRepository)
        {
            _mealDishRepository = mealDishRepository;
        }

        public async Task<IEnumerable<MealDishResponseDto>> Handle
            (GetAllMealDishesByUserIdAndDateAndMealTypeQuery request,
            CancellationToken cancellationToken)
        {
            var foundMealDishes = await _mealDishRepository.GetAllByAsync(mealDish =>
            mealDish.MealDetail.FoodDiary.UserId == request.UserId &&
            mealDish.MealDetail.Date == request.Date &&
            mealDish.MealDetail.MealType == request.MealType,
            cancellationToken);

            if (!foundMealDishes.Any())
            {
                throw new NotFoundException(MealDishErrorMessages.NoData);
            }

            var responseModel = foundMealDishes.Adapt<IEnumerable<MealDishResponseDto>>();

            return responseModel;
        }
    }
}
