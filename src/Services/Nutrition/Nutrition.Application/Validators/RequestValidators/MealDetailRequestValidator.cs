using FluentValidation;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Validators.CustomValidators;

namespace Nutrition.Application.Validators.RequestValidators
{
    public class MealDetailRequestValidator : AbstractValidator<MealDetailRequestDto>
    {
        public MealDetailRequestValidator()
        {
            RuleFor(mealDetial => mealDetial.MealType)
                .NotNull()
                .IsInEnum();

            RuleFor(mealDetial => mealDetial.Date)
                .NotNull()
                .NotEmpty()
                .Must(DateValidator.BeValidDate);
        }

    }
}
