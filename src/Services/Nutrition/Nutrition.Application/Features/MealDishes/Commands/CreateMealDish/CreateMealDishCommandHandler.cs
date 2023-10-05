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

        public CreateMealDishCommandHandler(IMealDishRepository mealDishRepository,
            IMealDetailRepository mealDetailRepository)
        {
            _mealDishRepository = mealDishRepository;
            _mealDetailRepository = mealDetailRepository;
        }

        public async Task<MealDishResponseDto> Handle(CreateMealDishCommand request,
            CancellationToken cancellationToken)
        {
            var mealDish = request.MealDishRequestDto.Adapt<MealDish>();
            mealDish.Id = Guid.NewGuid();

            _mealDishRepository.Create(mealDish);

            var isFoundMealDetail = await _mealDetailRepository.MealDetailExistsAsync(mealDish.MealDetail.Date,
                mealDish.MealDetail.MealType, cancellationToken);

            if (!isFoundMealDetail)
            {
                mealDish.MealDetail.Id = Guid.NewGuid();
                _mealDetailRepository.Create(mealDish.MealDetail);
            }

            await _mealDetailRepository.SaveChangesAsync(cancellationToken);

            var mealDishResponseDto = mealDish.Adapt<MealDishResponseDto>();

            return mealDishResponseDto;
        }
    }
}
