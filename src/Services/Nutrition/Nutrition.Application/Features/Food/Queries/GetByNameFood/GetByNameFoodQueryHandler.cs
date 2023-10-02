using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.Food.Queries.GetByNameFood
{
    public class GetByNameFoodQueryHandler : IRequestHandler<GetByNameFoodQuery, FoodResponseDto>
    {
        private readonly IFoodRepository _foodRepository;

        public GetByNameFoodQueryHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<FoodResponseDto> Handle(GetByNameFoodQuery request,
            CancellationToken cancellationToken)
        {
            var existingFood = await _foodRepository.GetByNameAsync(request.Name, cancellationToken);

            if (existingFood is null)
            {
                throw new NotFoundException(FoodErrorMessages.ProductNotFound);
            }

            var responseModel = existingFood.Adapt<FoodResponseDto>();

            return responseModel;
        }
    }
}
