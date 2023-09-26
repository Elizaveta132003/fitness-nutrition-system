namespace Identity.Application.Validators.CustomValidators
{
    public class DateOfBirthValidator
    {
        public static bool BeAValidDate(DateTime dateOfBirth)
        {
            DateTime currentDateMinus100Years = DateTime.Now.AddYears(-100);

            return dateOfBirth <= DateTime.Now && dateOfBirth >= currentDateMinus100Years;
        }
    }
}
