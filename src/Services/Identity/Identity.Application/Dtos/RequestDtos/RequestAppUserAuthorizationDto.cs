namespace Identity.Application.Dtos.RequestDtos
{
    /// <summary>
    /// DTO for user authorization request.
    /// </summary>
    public class RequestAppUserAuthorizationDto
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}
