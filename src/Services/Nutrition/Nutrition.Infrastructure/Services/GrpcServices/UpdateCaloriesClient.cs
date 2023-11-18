using Google.Protobuf.WellKnownTypes;
using Mapster;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Application.Interfaces.IGrpcService;
using Nutrition.Domain.Interfaces.IRepositories;
using Nutrition.Infrastructure.Protos;

namespace Nutrition.Infrastructure.Services.GrpcServices
{
    public class UpdateCaloriesClient : IUpdateCaloriesClient
    {
        private readonly CaloriesService.CaloriesServiceClient _caloriesServiceClient;
        private readonly IFoodRepository _foodRepository;

        public UpdateCaloriesClient(IFoodRepository foodRepository, CaloriesService.CaloriesServiceClient client)
        {
            _caloriesServiceClient = client;
            _foodRepository = foodRepository;
        }

        public async Task UpdateCaloriesAsync(MealDishRequestDto mealDishRequestDto,
            CancellationToken cancellationToken = default)
        {
            var food = await GetFoodByIdAsync(mealDishRequestDto.FoodId);

            var request = new UpdateCaloriesRequest
            {
                Calories = mealDishRequestDto.ServingSize * food.Calories,
                UserId = mealDishRequestDto.MealDetail!.FoodDiary.UserId.ToString(),
                Date = mealDishRequestDto.MealDetail.Date.ToTimestamp(),
                IsCaloriesConsumed = true
            };

            _ = await _caloriesServiceClient.UpdateCaloriesAsync(request);
        }
        private async Task<FoodResponseDto> GetFoodByIdAsync(Guid id)
        {
            var existingFood = await _foodRepository.GetOneByAsync(food => food.Id == id);

            if (existingFood is null)
            {
                throw new NotFoundException(FoodErrorMessages.ProductNotFound);
            }

            var responseModel = existingFood.Adapt<FoodResponseDto>();

            return responseModel;
        }
    }
}
