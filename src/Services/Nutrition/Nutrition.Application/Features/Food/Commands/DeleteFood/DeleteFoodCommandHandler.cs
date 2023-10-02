using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Food.Commands.DeleteFood
{
    public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, FoodResponseDto>
    {
        private readonly IFoodRepository _foodRepository;

        public DeleteFoodCommandHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<FoodResponseDto> Handle(DeleteFoodCommand request,
            CancellationToken cancellationToken)
        {
            var foundFood = await _foodRepository.GetByIdAsync(request.Id, cancellationToken);

            if (foundFood is null)
            {
                throw new NotFoundException(FoodErrorMessages.ProductNotFound);
            }

            await _foodRepository.DeleteAsync(foundFood, cancellationToken);

            await _foodRepository.SaveChangesAsync(cancellationToken);

            var foodResponseDto = foundFood.Adapt<FoodResponseDto>();

            return foodResponseDto;
        }
    }
}
