using FluentValidation;
using Identity.Application.Dtos.RequestDtos;

namespace Identity.Application.Validators.RequestValidators
{
    public class RequestAppUserAuthorizationValidator
        :AbstractValidator<RequestAppUserAuthorizationDto>
    {
        public RequestAppUserAuthorizationValidator()
        {
            RuleFor(user=>user.UserName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username is required.")
                .MaximumLength(50)
                .WithMessage("Username must not exceed 50 characters.");
        }
    }
}
