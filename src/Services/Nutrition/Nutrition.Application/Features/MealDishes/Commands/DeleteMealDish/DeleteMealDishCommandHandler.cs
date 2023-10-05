using Mapster;
using MediatR;
using Nutrition.Application.Dtos.ResponseDtos;
using Nutrition.Application.Exceptions;
using Nutrition.Application.Helpers;
using Nutrition.Domain.Interfaces.IRepositories;

namespace Nutrition.Application.Features.MealDishes.Commands.DeleteMealDish
{
    public class DeleteMealDishCommandHandler : IRequestHandler<DeleteMealDishCommand, MealDishResponseDto>
    {
        private readonly IMealDishRepository _mealDishRepository;

        public DeleteMealDishCommandHandler(IMealDishRepository mealDishRepository)
        {
            _mealDishRepository = mealDishRepository;
        }

        public async Task<MealDishResponseDto> Handle(DeleteMealDishCommand request,
            CancellationToken cancellationToken)
        {
            var foundMealDish = await _mealDishRepository.GetByIdAsync(request.Id);

            if (foundMealDish is null)
            {
                throw new NotFoundException(MealDishErrorMessages.MealDishNotFound);
            }

            _mealDishRepository.Delete(foundMealDish);

            await _mealDishRepository.SaveChangesAsync(cancellationToken);

            var mealDishResponseDto = foundMealDish.Adapt<MealDishResponseDto>();

            return mealDishResponseDto;
        }
    }
}
