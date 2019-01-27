namespace Feeder.Web.API.Helpers
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string UserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(FeederClaimTypes.UserId)?.Value;
        }
    }

    public class FeederClaimTypes
    {
        public const string UserId = "feeder.com.UserId";
    }
}
