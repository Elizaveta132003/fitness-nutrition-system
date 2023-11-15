using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Food.Commands.CreateFood
{
    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, FoodResponseDto>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly ILogger<CreateFoodCommandHandler> _logger;

        public CreateFoodCommandHandler(IFoodRepository foodRepository, ILogger<CreateFoodCommandHandler> logger)
        {
            _foodRepository = foodRepository;
            _logger = logger;
        }

        public async Task<FoodResponseDto> Handle(CreateFoodCommand request,
            CancellationToken cancellationToken)
        {
            var foundFood = await _foodRepository.GetOneByAsync(food => food.Name == request.FoodRequestDto.Name,
                cancellationToken);

            if (foundFood is not null)
            {
                _logger.LogInformation($"Product {foundFood.Name} already exists");

                throw new AlreadyExistsException(FoodErrorMessages.ProductAlreadyExists);
            }

            var food = request.FoodRequestDto.Adapt<Domain.Entities.Food>();
            food.Id = Guid.NewGuid();

            _foodRepository.Create(food);

            await _foodRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Product {food.Name} was successfully created");

            var foodResponseDto = food.Adapt<FoodResponseDto>();

            return foodResponseDto;
        }
    }
}
