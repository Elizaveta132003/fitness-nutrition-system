using FluentValidation;
using Nutrition.Application.Dtos.RequestDtos;

namespace Nutrition.Application.Validators.RequestValidators
{
    public class MealDishRequestValidator : AbstractValidator<MealDishRequestDto>
    {
        public MealDishRequestValidator()
        {
            RuleFor(mealDish => mealDish.ServingSize)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("ServingSize must be greater than or equal to zero");
        }
    }
}
