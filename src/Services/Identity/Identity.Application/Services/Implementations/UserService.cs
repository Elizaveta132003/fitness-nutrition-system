using Identity.Application.Dtos.RequestDtos;
using Identity.Application.Dtos.ResponseDtos;
using Identity.Application.Exceptions;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Services.Implementations
{
    /// <summary>
    /// Service for managing user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<IUserService> _logger;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the UserService class.
        /// </summary>
        /// <param name="userManager">The user manager for managing user data.</param>
        /// <param name="logger">The logger for logging service events.</param>
        /// <param name="tokenService">The token service for creating authentication tokens.</param>
        public UserService(UserManager<AppUser> userManager, ILogger<IUserService> logger,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _logger = logger;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Adds a new user based on the provided registration data.
        /// </summary>
        /// <param name="requestAppUserRegisterDto">The registration data of the user.</param>
        /// <returns>The registered user's information.</returns>
        /// <exception cref="BadRequestException">Thrown when user registration fails.</exception>
        public async Task<ResponseAppUserRegisterDto> AddUserAsync(RequestAppUserRegisterDto requestAppUserRegisterDto)
        {
            var user = requestAppUserRegisterDto.Adapt<AppUser>();
            var identityResult = await _userManager.CreateAsync(user, requestAppUserRegisterDto.Password);

            if (!identityResult.Succeeded)
            {

                _logger.LogInformation("User with id {Id} was not successfully created", user.Id);

                throw new BadRequestException(identityResult.Errors.First().Code);
            }

            await _userManager.AddToRoleAsync(user, "user");
            var result = user.Adapt<ResponseAppUserRegisterDto>();

            return result;
        }

        /// <summary>
        /// Deletes a user with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        /// <returns>The deleted user's information.</returns>
        /// <exception cref="NotFoundException">Thrown when the user with the specified ID is not found.</exception>
        public async Task<ResponseAppUserDto> DeleteUserAsync(Guid id)
        {
            var existingUser = await _userManager.FindByIdAsync(id.ToString());

            if (existingUser == null)
            {
                _logger.LogError($"User with ID {id} not found.");

                throw new NotFoundException("This id was not found.");
            }

            var responseModel = existingUser.Adapt<ResponseAppUserDto>();
            await _userManager.DeleteAsync(existingUser);

            return responseModel;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of user information.</returns>
        public async Task<List<ResponseAppUserDto>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null || users.Count == 0)
            {
                _logger.LogInformation("No users found.");

                return new List<ResponseAppUserDto>();
            }

            var usersDto = users.Adapt<List<ResponseAppUserDto>>();
            _logger.LogInformation("Users was successfully received.");

            return usersDto;
        }

        /// <summary>
        /// Performs user authorization based on the provided authorization data.
        /// </summary>
        /// <param name="requestAppUserAuthorizationDto">The authorization data of the user.</param>
        /// <returns>The user's authorization information.</returns>
        /// <exception cref="NotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when the user password is incorrect.</exception>
        public async Task<ResponseAppUserAuthorizationDto> UserAuthorizationAsync(RequestAppUserAuthorizationDto requestAppUserAuthorizationDto)
        {
            var existingUser = await _userManager.FindByNameAsync(requestAppUserAuthorizationDto.UserName);

            if (existingUser == null)
            {
                throw new NotFoundException("User not found.");
            }

            var checkPassword = await _userManager.CheckPasswordAsync(existingUser, requestAppUserAuthorizationDto.Password);

            if (!checkPassword)
            {
                throw new UnauthorizedAccessException("User password is incorrect.");
            }

            var token = await _tokenService.CreateTokenAsync(existingUser);
            var resultModel = existingUser.Adapt<ResponseAppUserAuthorizationDto>();
            resultModel.Token = token;

            return resultModel;
        }
    }
}