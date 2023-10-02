using FluentValidation;
using Nutrition.Application.Validators.RequestValidators;

namespace Nutrition.Application.Features.MealDishes.Commands.CreateMealDish
{
    public class CreateMealDishCommandValidator : AbstractValidator<CreateMealDishCommand>
    {
        public CreateMealDishCommandValidator()
        {
            RuleFor(x => x.MealDishRequestDto)
                .SetValidator(new MealDishRequestValidator());

            RuleFor(x => x.MealDishRequestDto.MealDetailRequestDto)
                .SetValidator(new MealDetailRequestValidator());
        }
    }
}
