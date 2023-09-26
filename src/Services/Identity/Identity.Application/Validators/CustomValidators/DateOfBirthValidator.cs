namespace Identity.Application.Validators.CustomValidators
{
    /// <summary>
    /// Validator class for validating the format of a date of birth.
    /// </summary>
    public class DateOfBirthValidator
    {
        /// <summary>
        /// Validates whether a given date of birth is within a valid range.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth to be validated.</param>
        /// <returns>True if the date of birth is valid; otherwise, false.</returns>
        public static bool BeAValidDate(DateTime dateOfBirth)
        {
            DateTime currentDateMinus100Years = DateTime.Now.AddYears(-100);

            return dateOfBirth <= DateTime.Now && dateOfBirth >= currentDateMinus100Years;
        }
    }
}
