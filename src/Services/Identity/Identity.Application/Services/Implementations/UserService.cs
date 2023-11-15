using Identity.Application.Dtos.RequestDtos;
using Identity.Application.Dtos.ResponseDtos;
using Identity.Application.Exceptions;
using Identity.Application.Helpers;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using Identity.Domain.Enums;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Kafka;
using Shared.Kafka.Enums;
using Shared.Kafka.Messages;

namespace Identity.Application.Services.Implementations
{
    /// <summary>
    /// Service for managing user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenService _tokenService;
        private readonly IKafkaMessageBus<string, UserMessage> _bus;

        /// <summary>
        /// Initializes a new instance of the UserService class.
        /// </summary>
        /// <param name="userManager">The user manager for managing user data.</param>
        /// <param name="logger">The logger for logging service events.</param>
        /// <param name="tokenService">The token service for creating authentication tokens.</param>
        public UserService(UserManager<AppUser> userManager, ILogger<UserService> logger,
            ITokenService tokenService, IKafkaMessageBus<string, UserMessage> bus)
        {
            _userManager = userManager;
            _logger = logger;
            _tokenService = tokenService;
            _bus = bus;
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
                _logger.LogInformation("User registration failed.");

                throw new BadRequestException(ErrorMessages.UserRegistrationFailed);
            }

            await _userManager.AddToRoleAsync(user, Role.user.ToString());

            _logger.LogInformation($"User {user.UserName} was successfully created");

            var createUserMessage = new UserMessage()
            {
                Id = user.Id,
                Username = user.UserName!,
                MessageType = MessageType.Create
            };

            await _bus.PublishAsync(user.UserName!, createUserMessage);

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
                _logger.LogInformation($"User with id {id} not found.");

                throw new NotFoundException(ErrorMessages.UserIdNotFound);
            }

            var responseModel = existingUser.Adapt<ResponseAppUserDto>();
            await _userManager.DeleteAsync(existingUser);

            _logger.LogInformation($"User {existingUser.UserName} with id {existingUser.Id} was successfully deleted");

            var deleteUserMessage = new UserMessage()
            {
                Id = existingUser.Id,
                MessageType = MessageType.Delete
            };

            await _bus.PublishAsync(existingUser.UserName, deleteUserMessage);

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

            _logger.LogInformation("Users was successfully received");

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
                _logger.LogInformation("User not found for authorization");

                throw new NotFoundException(ErrorMessages.UserNotFound);
            }

            var checkPassword = await _userManager.CheckPasswordAsync(existingUser, requestAppUserAuthorizationDto.Password);

            if (!checkPassword)
            {
                _logger.LogInformation($"Incorrect password for user {existingUser.UserName}");

                throw new UnauthorizedAccessException(ErrorMessages.IncorrectPassword);
            }

            _logger.LogInformation($"User {existingUser.UserName} successfully authorized");

            var token = await _tokenService.CreateTokenAsync(existingUser);
            var resultModel = existingUser.Adapt<ResponseAppUserAuthorizationDto>();
            resultModel.Token = token;

            return resultModel;
        }
    }
}