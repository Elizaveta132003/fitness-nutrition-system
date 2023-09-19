namespace Identity.Application.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a resource is not found.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the NotFoundException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public NotFoundException(string message) : base(message)
        { }
    }
}
