using Identity.Domain.Enums;

namespace Identity.Application.Dtos.ResponseDtos
{
    /// <summary>
    /// DTO for user registration response.
    /// </summary>
    public class ResponseAppUserRegisterDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the registered user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the registered user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the registered user.
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the registered user.
        /// </summary>
        public Gender Gender { get; set; }
    }
}