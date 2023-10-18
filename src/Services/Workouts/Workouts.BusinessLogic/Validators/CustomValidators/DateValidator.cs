namespace Workouts.BusinessLogic.Validators.CustomValidators
{
    public class DateValidator
    {
        public static bool BeValidDate(DateTime date)
        {
            DateTime currentDateMinus100Years = DateTime.Now.AddYears(-10);

            return date <= DateTime.Now && date >= currentDateMinus100Years;
        }
    }
}
