using FluentValidation;
using Nutrition.Application.Dtos.RequestDtos;

namespace Nutrition.Application.Validators.RequestValidators
{
    public class FoodRequestValidator: AbstractValidator<FoodRequestDto>
    {
        public FoodRequestValidator()
        {
            RuleFor(food => food.Name)
                .NotEmpty()
                .WithMessage("The food name cannot be empty.")
                .NotNull()
                .WithMessage("The food name cannot be null.");

            RuleFor(food => food.Calories)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Calories cannot be less than zero.");

        }
    }
}
