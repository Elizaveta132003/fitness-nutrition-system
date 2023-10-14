using FluentValidation;
using Workouts.BusinessLogic.Dtos.RequestDtos;

namespace Workouts.BusinessLogic.Validators.RequestValidators
{
    public class WorkoutExerciseValidator : AbstractValidator<WorkoutExerciseRequestDto>
    {
        public WorkoutExerciseValidator()
        {
            RuleFor(x => x.Duration)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .WithMessage("Duration must be greater than or equal to one");
        }
    }
}
