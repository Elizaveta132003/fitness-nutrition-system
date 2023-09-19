namespace Identity.Application.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a bad request is encountered.
    /// </summary>
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the BadRequestException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public BadRequestException(string message) : base(message)
        { }
    }
}
