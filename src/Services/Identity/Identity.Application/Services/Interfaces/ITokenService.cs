using Identity.Domain.Entities;

namespace Identity.Application.Services.Interfaces
{
    /// <summary>
    /// Provides methods for working with authentication tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates an authentication token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is created.</param>
        /// <returns>The generated authentication token as a string.</returns>
        Task<string> CreateTokenAsync(AppUser user);
    }
}
