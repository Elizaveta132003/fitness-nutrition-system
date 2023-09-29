using FluentValidation;

namespace Nutrition.Application.Features.Food.Commands.DeleteFood
{
    public class DeleteFoodCommandValidator:AbstractValidator<DeleteFoodCommand>
    {
        public DeleteFoodCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty()
                .NotNull();
        }
    }
}
