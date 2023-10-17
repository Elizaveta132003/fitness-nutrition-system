using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Food.Queries.GetAllFood
{
    public class GetAllFoodQueryHandler : IRequestHandler<GetAllFoodQuery, IEnumerable<FoodResponseDto>>
    {
        private readonly IFoodRepository _foodRepository;

        public GetAllFoodQueryHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<IEnumerable<FoodResponseDto>> Handle(GetAllFoodQuery request,
            CancellationToken cancellationToken)
        {
            var food = await _foodRepository.GetAllFoodAsync(cancellationToken);

            var foodResponseDto = food.Adapt<IEnumerable<FoodResponseDto>>();

            return foodResponseDto;
        }
    }
}
