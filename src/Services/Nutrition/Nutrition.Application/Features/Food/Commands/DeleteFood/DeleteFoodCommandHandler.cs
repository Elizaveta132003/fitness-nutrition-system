using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Food.Commands.DeleteFood
{
    public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, FoodResponseDto>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly ILogger<DeleteFoodCommandHandler> _logger;

        public DeleteFoodCommandHandler(IFoodRepository foodRepository, ILogger<DeleteFoodCommandHandler> logger)
        {
            _foodRepository = foodRepository;
            _logger = logger;
        }

        public async Task<FoodResponseDto> Handle(DeleteFoodCommand request,
            CancellationToken cancellationToken)
        {
            var foundFood = await _foodRepository.GetOneByAsync(food => food.Id == request.Id, cancellationToken);

            if (foundFood is null)
            {
                _logger.LogInformation($"Product with id {request.Id} not found");

                throw new NotFoundException(FoodErrorMessages.ProductNotFound);
            }

            _foodRepository.Delete(foundFood);

            await _foodRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Product {foundFood.Name} was successfully deleted");

            var foodResponseDto = foundFood.Adapt<FoodResponseDto>();

            return foodResponseDto;
        }
    }
}
