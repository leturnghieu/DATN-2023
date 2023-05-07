using DATN.Models;
using System.Security.Claims;

namespace DATN.Extentions
{
    public static class UserExtentions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            string userID = (principal.FindFirstValue(ClaimTypes.NameIdentifier));
            return userID;
        }
        public static string GetUserRole(this ClaimsPrincipal principal)
        {
            string role = principal.FindFirstValue(ClaimTypes.Role);
            return role;
        }
    }
}
