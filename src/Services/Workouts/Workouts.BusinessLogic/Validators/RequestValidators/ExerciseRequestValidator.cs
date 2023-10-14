using FluentValidation;
using Workouts.BusinessLogic.Dtos.RequestDtos;

namespace Workouts.BusinessLogic.Validators.RequestValidators
{
    public class ExerciseRequestValidator : AbstractValidator<ExerciseRequestDto>
    {
        public ExerciseRequestValidator()
        {
            RuleFor(exercise => exercise.Name)
                .NotEmpty()
                .WithMessage("The exercise name cannot be empty.")
                .NotNull()
                .WithMessage("The exercise name cannot be null.");

            RuleFor(exercise => exercise.CaloriesBurnedPerMinute)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Calories cannot be less than zero.");

            RuleFor(exercise => exercise.ExerciseType)
                .NotNull()
                .IsInEnum();
        }
    }
}
