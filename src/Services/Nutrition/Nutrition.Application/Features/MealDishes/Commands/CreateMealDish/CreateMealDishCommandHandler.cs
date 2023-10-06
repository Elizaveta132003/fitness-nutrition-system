using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.MealDishes.Commands.CreateMealDish
{
    public class CreateMealDishCommandHandler : IRequestHandler<CreateMealDishCommand, MealDishResponseDto>
    {
        private readonly IMealDishRepository _mealDishRepository;
        private readonly IMealDetailRepository _mealDetailRepository;
        private readonly IFoodDiaryRepository _foodDiaryRepository;

        public CreateMealDishCommandHandler(IMealDishRepository mealDishRepository,
            IMealDetailRepository mealDetailRepository,
            IFoodDiaryRepository foodDiaryRepository)
        {
            _mealDishRepository = mealDishRepository;
            _mealDetailRepository = mealDetailRepository;
            _foodDiaryRepository = foodDiaryRepository;
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

            var mealDishResponseDto = mealDish.Adapt<MealDishResponseDto>();

            return mealDishResponseDto;
        }

        private async Task<MealDetail> GetMealDetail(MealDish mealDish, Guid userId, CancellationToken cancellationToken)
        {
            var isFoundMealDetail = await _mealDetailRepository.MealDetailExistsAsync(
                mealDish.MealDetail!.Date,
                mealDish.MealDetail.MealType,
                cancellationToken);

            if (!isFoundMealDetail)
            {
                return await CreateMealDetailAsync(mealDish, userId, cancellationToken);
            }

            return await _mealDetailRepository.GetByDateAndMealTypeAsync(
                mealDish.MealDetail.Date,
                mealDish.MealDetail.MealType,
                cancellationToken);
        }

        private async Task<MealDetail> CreateMealDetailAsync(MealDish mealDish, Guid userId, CancellationToken cancellationToken)
        {
            var foodDiary = await _foodDiaryRepository.GetByUserIdAsync(
                    userId,
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
