using Microsoft.Extensions.Configuration;

namespace Identity.Application.Configurations
{
    /// <summary>
    /// Represents the JWT settings used for authentication.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets the issuer of the JWT.
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// Gets the audience of the JWT.
        /// </summary>
        public string Audience { get; }

        /// <summary>
        /// Gets the token expiration time in minutes.
        /// </summary>
        public double ExpiryInMinutes { get; }

        /// <summary>
        ///  Gets the secret key used to sign the JWT.
        /// </summary>
        public string SecretKey { get; }

        /// <summary>
        /// Initializes a new instance of the JwtSettings class.
        /// </summary>
        /// <param name="configuration">The configuration containing JWT settings.</param>
        public JwtSettings(IConfiguration configuration)
        {
            Issuer = GetConfigurationValue(configuration, "JwtSettings:Issuer");
            Audience = GetConfigurationValue(configuration, "JwtSettings:Audience");
            ExpiryInMinutes = GetExpiryInMinutes(configuration);
            SecretKey = GetConfigurationValue(configuration, "JwtSettings:SecretKey");
        }

        /// <summary>
        /// Gets a configuration value by its key.
        /// </summary>
        /// <param name="configuration">The configuration source.</param>
        /// <param name="key">The key of the configuration value.</param>
        /// <returns>The configuration value associated with the key.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the key is not found in the configuration.</exception>
        private string GetConfigurationValue(IConfiguration configuration, string key)
        {
            string value = configuration.GetSection(key).Value;

            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"{key} not found in configuration");
            }

            return value;
        }

        /// <summary>
        /// Parses and retrieves the token expiration time in minutes from the configuration.
        /// </summary>
        /// <param name="configuration">The configuration source.</param>
        /// <returns>The token expiration time in minutes.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the expiration time token is invalid in the configuration.</exception>
        private double GetExpiryInMinutes(IConfiguration configuration)
        {
            string expiryTimeTokenString = configuration.GetSection("JwtSettings:ExpiryInMinutes").Value;

            if (!double.TryParse(expiryTimeTokenString, out double expiryTimeToken))
            {
                throw new InvalidOperationException("Invalid expiry time token in configuration");
            }

            return expiryTimeToken;
        }
    }
}
