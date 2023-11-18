using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Food.Commands.UpdateFood
{
    public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, FoodResponseDto>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly ILogger<UpdateFoodCommandHandler> _logger;
        public UpdateFoodCommandHandler(IFoodRepository foodRepository, ILogger<UpdateFoodCommandHandler> logger)
        {
            _foodRepository = foodRepository;
            _logger = logger;
        }

        public async Task<FoodResponseDto> Handle(UpdateFoodCommand request,
            CancellationToken cancellationToken)
        {
            var foundFood = await _foodRepository.GetOneByAsync(food => food.Name == request.FoodRequestDto.Name,
                cancellationToken);

            if (foundFood is null)
            {
                _logger.LogError($"Product {request.FoodRequestDto.Name} already exists");

                throw new NotFoundException(FoodErrorMessages.ProductAlreadyExists);
            }

            var food = request.FoodRequestDto.Adapt<Domain.Entities.Food>();
            food.Id = request.Id;

            _foodRepository.Update(food);

            await _foodRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Product {food.Name} was successfully updated");

            var foodResponseDto = food.Adapt<FoodResponseDto>();

            return foodResponseDto;
        }
    }
}
