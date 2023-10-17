using FluentValidation;
using Nutrition.Application.Dtos.RequestDtos;

namespace Nutrition.Application.Validators.RequestValidators
{
    public class UserRequestValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestValidator()
        {
            RuleFor(user => user.Username)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username is required.")
                .MaximumLength(50)
                .WithMessage("Username must not exceed 50 characters.");
        }
    }
}