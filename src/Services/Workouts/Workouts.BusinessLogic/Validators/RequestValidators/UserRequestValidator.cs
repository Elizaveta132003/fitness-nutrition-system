using FluentValidation;
using Workouts.BusinessLogic.Dtos.RequestDtos;

namespace Workouts.BusinessLogic.Validators.RequestValidators
{
    public class UserRequestValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username is required.")
                .MaximumLength(50)
                .WithMessage("Username must not exceed 50 characters.");
        }
    }
}
