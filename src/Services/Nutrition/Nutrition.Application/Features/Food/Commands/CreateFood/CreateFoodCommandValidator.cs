using FluentValidation;
using Nutrition.Application.Validators.RequestValidators;

namespace Nutrition.Application.Features.Food.Commands.CreateFood
{
    public class CreateFoodCommandValidator : AbstractValidator<CreateFoodCommand>
    {
        public CreateFoodCommandValidator() 
        { 
            RuleFor(x=>x.FoodRequestDto)
                .SetValidator(new FoodRequestValidator());
        }
    }
}
