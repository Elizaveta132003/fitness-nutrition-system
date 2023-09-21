namespace Identity.Application.Dtos.ResponseDtos
{
    /// <summary>
    /// DTO for user authorization response.
    /// </summary>
    public class ResponseAppUserAuthorizationDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the authorization token for the user.
        /// </summary>
        public string Token { get; set; }
    }
}
