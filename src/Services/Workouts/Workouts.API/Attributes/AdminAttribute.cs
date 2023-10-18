using Microsoft.AspNetCore.Authorization;
using Workouts.BusinessLogic.Enums;

namespace Workouts.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class AdminAttribute : AuthorizeAttribute
    {
        public AdminAttribute()
        {
            Roles = Role.Admin.ToString();
        }
    }
}
