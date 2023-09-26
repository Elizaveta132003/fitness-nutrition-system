using Identity.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Identity.API.Attributes
{
    /// <summary>
    /// Custom authorization attribute for allowing access only to users with the 'admin' role.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class AdminAttribute : AuthorizeAttribute
    {
        public AdminAttribute()
        {
            Roles = Role.admin.ToString();
        }
    }
}
