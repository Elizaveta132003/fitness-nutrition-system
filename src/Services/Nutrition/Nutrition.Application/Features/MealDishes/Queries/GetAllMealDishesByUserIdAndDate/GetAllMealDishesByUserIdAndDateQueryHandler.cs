using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetAllMealDishesByUserIdAndDateQueryHandler> _logger;

        public GetAllMealDishesByUserIdAndDateQueryHandler(IMealDishRepository mealDishRepository,
            ILogger<GetAllMealDishesByUserIdAndDateQueryHandler> logger)
        {
            _mealDishRepository = mealDishRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MealDishResponseDto>> Handle
            (GetAllMealDishesByUserIdAndDateQuery request, CancellationToken cancellationToken)
        {
            var foundMealDishes = await _mealDishRepository.GetAllByAsync(mealDish =>
            mealDish.MealDetail.FoodDiary.UserId == request.UserId &&
            mealDish.MealDetail.Date == request.Date,
            cancellationToken);

            if (!foundMealDishes.Any())
            {
                _logger.LogError($"No meal dishes found by user id {request.UserId} and date {request.Date}");

                throw new NotFoundException(MealDishErrorMessages.NoData);
            }

            _logger.LogInformation($"Meal dishes by user id {request.UserId} and date {request.Date} were successfully received");

            var responseModel = foundMealDishes.Adapt<IEnumerable<MealDishResponseDto>>();

            return responseModel;
        }
    }
}
