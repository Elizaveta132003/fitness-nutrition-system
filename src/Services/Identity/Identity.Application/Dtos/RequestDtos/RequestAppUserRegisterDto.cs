using Identity.Domain.Enums;

namespace Identity.Application.Dtos.RequestDtos
{
    /// <summary>
    /// DTO for user registration request.
    /// </summary>
    public class RequestAppUserRegisterDto
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the password for the user's account.
        /// </summary>
        public string Password { get; set; }
    }
}
