using FluentValidation;
using Identity.Application.Dtos.RequestDtos;

namespace Identity.Application.Validators.RequestValidators
{
    /// <summary>
    /// Validator class for validating authorization request data for an application user.
    /// </summary>
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
