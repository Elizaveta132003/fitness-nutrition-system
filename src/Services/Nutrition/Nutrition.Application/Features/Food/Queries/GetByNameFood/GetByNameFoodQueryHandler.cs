using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Food.Queries.GetByNameFood
{
    public class GetByNameFoodQueryHandler : IRequestHandler<GetByNameFoodQuery, FoodResponseDto>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly ILogger<GetByNameFoodQueryHandler> _logger;

        public GetByNameFoodQueryHandler(IFoodRepository foodRepository, ILogger<GetByNameFoodQueryHandler> logger)
        {
            _foodRepository = foodRepository;
            _logger = logger;
        }

        public async Task<FoodResponseDto> Handle(GetByNameFoodQuery request,
            CancellationToken cancellationToken)
        {
            var existingFood = await _foodRepository.GetOneByAsync(food => food.Name == request.Name,
                cancellationToken);

            if (existingFood is null)
            {
                throw new NotFoundException(FoodErrorMessages.ProductNotFound);
            }

            _logger.LogInformation($"Product by name {existingFood.Name} received");

            var responseModel = existingFood.Adapt<FoodResponseDto>();

            return responseModel;
        }
    }
}
