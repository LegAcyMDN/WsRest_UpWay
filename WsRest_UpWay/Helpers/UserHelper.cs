using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WsRest_UpWay.Helpers;

public static class UserHelper
{
    public static string GetEmail(this ClaimsPrincipal principal)
    {
        var userIdClaim = principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) ??
                          principal.FindFirst(c => c.Type == JwtRegisteredClaimNames.Sub);
        if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value)) return userIdClaim.Value;

        return null;
    }
}