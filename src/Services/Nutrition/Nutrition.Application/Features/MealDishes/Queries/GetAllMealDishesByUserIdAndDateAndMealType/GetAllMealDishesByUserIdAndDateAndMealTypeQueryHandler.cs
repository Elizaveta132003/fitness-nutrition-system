using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetAllMealDishesByUserIdAndDateAndMealTypeQueryHandler> _logger;

        public GetAllMealDishesByUserIdAndDateAndMealTypeQueryHandler(IMealDishRepository mealDishRepository,
            ILogger<GetAllMealDishesByUserIdAndDateAndMealTypeQueryHandler> logger)
        {
            _mealDishRepository = mealDishRepository;
            _logger = logger;
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
                _logger.LogInformation($"No meal dishes found by user id {request.UserId}, date {request.Date} and meal type {request.MealType}");

                throw new NotFoundException(MealDishErrorMessages.NoData);
            }

            _logger.LogInformation($"Meal dishes by user id {request.UserId}, date {request.Date} and meal type {request.MealType} are received");

            var responseModel = foundMealDishes.Adapt<IEnumerable<MealDishResponseDto>>();

            return responseModel;
        }
    }
}
