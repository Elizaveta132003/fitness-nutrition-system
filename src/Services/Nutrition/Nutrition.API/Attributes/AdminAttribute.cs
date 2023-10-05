using Microsoft.AspNetCore.Authorization;
using Nutrition.Domain.Enums;

namespace Nutrition.API.Attributes
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
