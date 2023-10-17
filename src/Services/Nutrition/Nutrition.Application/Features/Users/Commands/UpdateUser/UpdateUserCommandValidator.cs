using FluentValidation;
using Nutrition.Application.Validators.RequestValidators;

namespace Nutrition.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserRequestDto)
                .SetValidator(new UserRequestValidator());
        }
    }
}
