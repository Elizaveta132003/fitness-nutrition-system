using Identity.Application.Dtos.RequestDtos;
using Identity.Application.Dtos.ResponseDtos;
using Identity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    /// <summary>
    /// Controller for user-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the UsersController class.
        /// </summary>
        /// <param name="userService">The user service for managing user operations.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="requestAppUserRegisterDto">The registration data of the user.</param>
        /// <returns>The registered user's information.</returns>
        [HttpPost("sign-up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseAppUserRegisterDto>> RegisterUserAsync(RequestAppUserRegisterDto requestAppUserRegisterDto)
        {
            var serviceResult = await _userService.AddUserAsync(requestAppUserRegisterDto);

            return Ok(serviceResult);
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="requestAppUserAuthorizationDto">The user's login credentials.</param>
        /// <returns>The authorized user's information with a token.</returns>
        [HttpPost("sign-in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseAppUserAuthorizationDto>> LoginUserAsync(RequestAppUserAuthorizationDto requestAppUserAuthorizationDto)
        {
            var serviceResult = await _userService.UserAuthorizationAsync(requestAppUserAuthorizationDto);

            return Ok(serviceResult);
        }

        /// <summary>
        /// Gets a list of users.
        /// </summary>
        /// <returns>A list of user information.</returns>
        [Authorize(Roles = "admin")]
        [HttpGet("getUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ResponseAppUserDto>>> GetUsersAsync()
        {
            var serviceResult = await _userService.GetUsersAsync();

            return Ok(serviceResult);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The deleted user's information.</returns>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseAppUserDto>> DeleteUserAsync(Guid id)
        {
            var serviceResult = await _userService.DeleteUserAsync(id);

            return Ok(serviceResult);
        }
    }
}
