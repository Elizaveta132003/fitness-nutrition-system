using FluentValidation;
using Nutrition.Application.Validators.RequestValidators;

namespace Nutrition.Application.Features.Food.Commands.UpdateFood
{
    public class UpdateFoodCommandValidator : AbstractValidator<UpdateFoodCommand>
    {
        public UpdateFoodCommandValidator()
        {
            RuleFor(x => x.FoodRequestDto)
               .SetValidator(new FoodRequestValidator());
        }
    }
}
