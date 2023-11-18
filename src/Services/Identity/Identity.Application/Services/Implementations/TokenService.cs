using Identity.Application.Configurations;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Application.Services.Implementations
{
    /// <summary>
    /// Service for creating authentication tokens.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<TokenService> _logger;

        /// <summary>
        /// Initializes a new instance of the TokenService class.
        /// </summary>
        /// <param name="configuration">The configuration containing JWT settings.</param>
        /// <param name="userManager">The user manager for accessing user information.</param>
        public TokenService(IConfiguration configuration, UserManager<AppUser> userManager, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _jwtSettings = new JwtSettings(configuration);
            _logger = logger;
        }

        /// <summary>
        /// Creates an authentication token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is created.</param>
        /// <returns>The generated authentication token as a string.</returns>
        public async Task<string> CreateTokenAsync(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes),
                signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();

            _logger.LogInformation($"Authentication token created for user {user.UserName}");

            return tokenHandler.WriteToken(token);
        }
    }
}
