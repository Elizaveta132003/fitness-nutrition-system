using Calories.BusinessLogic.Dtos.RequestDtos;
using FluentValidation;

namespace Calories.BusinessLogic.Validators
{
    public class CaloriesTrackingRequestValidator : AbstractValidator<CaloriesTrackingRequestDto>
    {
        public CaloriesTrackingRequestValidator()
        {
            RuleFor(x => x.CaloriesConsumed)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.CaloriesBurned)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Date)
                .NotEmpty()
                .NotNull();
        }
    }
}
