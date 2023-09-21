namespace Identity.Application.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an entity or item already exists.
    /// </summary>
    public class AlreadyExistsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the AlreadyExistsException class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public AlreadyExistsException(string message) : base(message)
        { }
    }
}