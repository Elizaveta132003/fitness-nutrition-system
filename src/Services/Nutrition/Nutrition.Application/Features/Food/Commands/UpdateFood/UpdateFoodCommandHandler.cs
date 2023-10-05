using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Features.Food.Commands.CreateFood;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Food.Commands.UpdateFood
{
    public class UpdateFoodCommandHandler : IRequestHandler<CreateFoodCommand, FoodResponseDto>
    {
        private readonly IFoodRepository _foodRepository;
        public UpdateFoodCommandHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<FoodResponseDto> Handle(CreateFoodCommand request,
            CancellationToken cancellationToken)
        {
            var isFoundFood = await _foodRepository.FoodExistsAsync(
                request.FoodRequestDto.Name, cancellationToken);

            if (isFoundFood)
            {
                throw new NotFoundException(FoodErrorMessages.ProductAlreadyExists);
            }

            var food = request.FoodRequestDto.Adapt<Domain.Entities.Food>();

            _foodRepository.Update(food);

            await _foodRepository.SaveChangesAsync(cancellationToken);

            var foodResponseDto = food.Adapt<FoodResponseDto>();

            return foodResponseDto;
        }
    }
}
