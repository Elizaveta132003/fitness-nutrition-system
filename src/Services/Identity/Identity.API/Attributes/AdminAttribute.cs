using Identity.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Identity.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class AdminAttribute : AuthorizeAttribute
    {
        public AdminAttribute()
        {
            Roles = Role.admin.ToString();
        }
    }
}
