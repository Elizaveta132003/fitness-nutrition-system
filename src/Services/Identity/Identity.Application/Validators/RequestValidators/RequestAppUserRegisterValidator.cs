using FluentValidation;
using Identity.Application.Dtos.RequestDtos;
using Identity.Application.Validators.CustomValidators;

namespace Identity.Application.Validators.RequestValidators
{
    public class RequestAppUserRegisterValidator: AbstractValidator<RequestAppUserRegisterDto>
    {
        public RequestAppUserRegisterValidator() 
        {
            RuleFor(user => user.UserName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username is required.")
                .MaximumLength(50)
                .WithMessage("Username must not exceed 50 characters.");

            RuleFor(user => user.DateOfBirth)
                .NotEmpty()
                .NotNull()
                .Must(DateOfBirthValidator.BeAValidDate)
                .WithMessage("Date of birth must be a valid date and not in the future.");

            RuleFor(user=>user.Gender)
                .IsInEnum()
                .WithMessage("Gender must be one of the valid values: Male, Female, Other.");
        }

    }
}
