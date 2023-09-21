using Identity.Application.Dtos.RequestDtos;
using Identity.Application.Dtos.ResponseDtos;

namespace Identity.Application.Services.Interfaces
{
    /// <summary>
    /// Provides methods for user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Adds a new user based on the provided registration data.
        /// </summary>
        /// <param name="requestAppUserRegisterDto">The registration data of the user.</param>
        /// <returns>The registered user's information.</returns>
        Task<ResponseAppUserRegisterDto> AddUserAsync(RequestAppUserRegisterDto requestAppUserRegisterDto);

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of user information.</returns>
        Task<List<ResponseAppUserDto>> GetUsersAsync();

        /// <summary>
        /// Performs user authorization based on the provided authorization data.
        /// </summary>
        /// <param name="requestAppUserAuthorizationDto">The authorization data of the user.</param>
        /// <returns>The user's authorization information.</returns>
        Task<ResponseAppUserAuthorizationDto> UserAuthorizationAsync(RequestAppUserAuthorizationDto requestAppUserAuthorizationDto);

        /// <summary>
        /// Deletes a user with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        /// <returns>The deleted user's information.</returns>
        Task<ResponseAppUserDto> DeleteUserAsync(Guid id);
    }
}