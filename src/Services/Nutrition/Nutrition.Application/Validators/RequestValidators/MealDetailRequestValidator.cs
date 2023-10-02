using FluentValidation;
using Nutrition.Application.Dtos.RequestDtos;
using Nutrition.Application.Validators.CustomValidators;

namespace Nutrition.Application.Validators.RequestValidators
{
    public class MealDetailRequestValidator : AbstractValidator<MealDetailRequestDto>
    {
        public MealDetailRequestValidator()
        {
            RuleFor(x => x.MealType)
                .NotNull()
                .IsInEnum();

            RuleFor(x => x.Date)
                .NotNull()
                .NotEmpty()
                .Must(DateValidator.BeValidDate);
        }

    }
}
