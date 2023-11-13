using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Interfaces.IGrpcService;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.MealDishes.Commands.CreateMealDish
{
    public class CreateMealDishCommandHandler : IRequestHandler<CreateMealDishCommand, MealDishResponseDto>
    {
        private readonly IMealDishRepository _mealDishRepository;
        private readonly IMealDetailRepository _mealDetailRepository;
        private readonly IFoodDiaryRepository _foodDiaryRepository;
        private readonly IUpdateCaloriesClient _updateCaloriesClient;
        private readonly ILogger<CreateMealDishCommandHandler> _logger;

        public CreateMealDishCommandHandler(IMealDishRepository mealDishRepository,
            IMealDetailRepository mealDetailRepository,
            IFoodDiaryRepository foodDiaryRepository,
            IUpdateCaloriesClient updateCaloriesClient,
            ILogger<CreateMealDishCommandHandler> logger)
        {
            _mealDishRepository = mealDishRepository;
            _mealDetailRepository = mealDetailRepository;
            _foodDiaryRepository = foodDiaryRepository;
            _updateCaloriesClient = updateCaloriesClient;
            _logger = logger;
        }

        public async Task<MealDishResponseDto> Handle(CreateMealDishCommand request,
            CancellationToken cancellationToken)
        {
            var mealDish = request.MealDishRequestDto.Adapt<MealDish>();
            mealDish.Id = Guid.NewGuid();

            var userId = request.MealDishRequestDto.MealDetail!.FoodDiary.UserId;
            var mealDetail = await GetMealDetail(mealDish, userId, cancellationToken);

            mealDish.MealDetailId = mealDetail.Id;
            mealDish.MealDetail = null;

            _mealDishRepository.Create(mealDish);

            await _mealDetailRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Meal dish is created");

            await _updateCaloriesClient.UpdateCaloriesAsync(request.MealDishRequestDto, cancellationToken);

            var mealDishResponseDto = mealDish.Adapt<MealDishResponseDto>();

            return mealDishResponseDto;
        }

        private async Task<MealDetail> GetMealDetail(MealDish mealDish, Guid userId, CancellationToken cancellationToken)
        {
            var foundMealDetail = await _mealDetailRepository.GetOneByAsync(mealDetail =>
            mealDetail.Date == mealDish.MealDetail!.Date &&
            mealDetail.MealType == mealDish.MealDetail.MealType,
            cancellationToken);

            if (foundMealDetail is null)
            {
                return await CreateMealDetailAsync(mealDish, userId, cancellationToken);
            }

            var existingMealDetail = await _mealDetailRepository.GetOneByAsync(mealDetail =>
            mealDetail.Date == mealDish.MealDetail!.Date &&
            mealDetail.MealType == mealDish.MealDetail.MealType,
            cancellationToken);

            return existingMealDetail;
        }

        private async Task<MealDetail> CreateMealDetailAsync(MealDish mealDish, Guid userId, CancellationToken cancellationToken)
        {
            var foodDiary = await _foodDiaryRepository.GetOneByAsync(foodDiary => foodDiary.UserId == userId,
                cancellationToken);

            var mealDetail = new MealDetail()
            {
                Id = Guid.NewGuid(),
                FoodDiaryId = foodDiary.Id,
                Date = mealDish.MealDetail!.Date,
                MealType = mealDish.MealDetail.MealType
            };

            _mealDetailRepository.Create(mealDetail);

            return mealDetail;
        }
    }
}
