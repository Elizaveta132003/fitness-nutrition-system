using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.MealDishes.Commands.DeleteMealDish
{
    public class DeleteMealDishCommandHandler : IRequestHandler<DeleteMealDishCommand, MealDishResponseDto>
    {
        private readonly IMealDishRepository _mealDishRepository;
        private readonly ILogger<DeleteMealDishCommandHandler> _logger;

        public DeleteMealDishCommandHandler(IMealDishRepository mealDishRepository, ILogger<DeleteMealDishCommandHandler> logger)
        {
            _mealDishRepository = mealDishRepository;
            _logger = logger;
        }

        public async Task<MealDishResponseDto> Handle(DeleteMealDishCommand request,
            CancellationToken cancellationToken)
        {
            var foundMealDish = await _mealDishRepository.GetOneByAsync(mealDish => mealDish.Id == request.Id,
                cancellationToken);

            if (foundMealDish is null)
            {
                throw new NotFoundException(MealDishErrorMessages.MealDishNotFound);
            }

            _mealDishRepository.Delete(foundMealDish);

            await _mealDishRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Meal dish is removed");

            var mealDishResponseDto = foundMealDish.Adapt<MealDishResponseDto>();

            return mealDishResponseDto;
        }
    }
}
