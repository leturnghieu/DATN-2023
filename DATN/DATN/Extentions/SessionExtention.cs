using Microsoft.AspNetCore.Mvc;

namespace DATN.Extentions
{
    public static class SessionExtention
    {
        public static string? GetUserName(this ISession session)
        {
            return session.GetString("UserName");
        }
        public static int? GetUserId(this ISession session)
        {
            return session.GetInt32("UserId");
        }
        public static int? GetUserRole(this ISession session)
        {
            return session.GetInt32("Role");
        }
        public static int? GetCsntId(this ISession session)
        {
            return session.GetInt32("CsntId");
        }
    }
}
