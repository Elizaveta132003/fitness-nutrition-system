using FluentValidation;
using Workouts.BusinessLogic.Dtos.RequestDtos;
using Workouts.BusinessLogic.Validators.CustomValidators;

namespace Workouts.BusinessLogic.Validators.RequestValidators
{
    public class WorkoutRequestValidator : AbstractValidator<WorkoutRequestDto>
    {
        public WorkoutRequestValidator()
        {
            RuleFor(x => x.Date)
                .NotNull()
                .NotEmpty()
                .Must(DateValidator.BeValidDate);
        }
    }
}
