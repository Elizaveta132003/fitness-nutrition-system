using Identity.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities
{
    /// <summary>
    /// Represents a user entity.
    /// </summary>
    public class AppUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public Gender Gender { get; set; }
    }
}
