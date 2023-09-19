namespace Identity.Application.Dtos.ResponseDtos
{
    /// <summary>
    /// DTO for user response.
    /// </summary>
    public class ResponseAppUserDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string UserName { get; set; }
    }
}