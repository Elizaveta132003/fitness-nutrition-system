using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Food.Commands.UpdateFood
{
    public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, FoodResponseDto>
    {
        private readonly IFoodRepository _foodRepository;
        public UpdateFoodCommandHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<FoodResponseDto> Handle(UpdateFoodCommand request,
            CancellationToken cancellationToken)
        {
            var foundFood = await _foodRepository.GetOneByAsync(food => food.Name == request.FoodRequestDto.Name,
                cancellationToken);

            if (foundFood is null)
            {
                throw new NotFoundException(FoodErrorMessages.ProductAlreadyExists);
            }

            var food = request.FoodRequestDto.Adapt<Domain.Entities.Food>();
            food.Id = request.Id;

            _foodRepository.Update(food);

            await _foodRepository.SaveChangesAsync(cancellationToken);

            var foodResponseDto = food.Adapt<FoodResponseDto>();

            return foodResponseDto;
        }
    }
}
