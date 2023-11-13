using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Food.Queries.GetAllFood
{
    public class GetAllFoodQueryHandler : IRequestHandler<GetAllFoodQuery, IEnumerable<FoodResponseDto>>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly ILogger<GetAllFoodQueryHandler> _logger;

        public GetAllFoodQueryHandler(IFoodRepository foodRepository, ILogger<GetAllFoodQueryHandler> logger)
        {
            _foodRepository = foodRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<FoodResponseDto>> Handle(GetAllFoodQuery request,
            CancellationToken cancellationToken)
        {
            var food = await _foodRepository.GetAllFoodAsync(cancellationToken);

            _logger.LogInformation("Products received");

            var foodResponseDto = food.Adapt<IEnumerable<FoodResponseDto>>();

            return foodResponseDto;
        }
    }
}
