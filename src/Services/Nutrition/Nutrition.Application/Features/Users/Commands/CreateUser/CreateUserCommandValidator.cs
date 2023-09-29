using FluentValidation;
using Nutrition.Application.Validators.RequestValidators;

namespace Nutrition.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() 
        {
            RuleFor(x => x.UserRequestDto)
                .SetValidator(new UserRequestValidator());
        }
    }
}
