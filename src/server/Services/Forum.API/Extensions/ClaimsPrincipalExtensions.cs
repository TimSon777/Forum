// ReSharper disable once CheckNamespace
namespace System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static bool IsAdmin(this ClaimsPrincipal principal)
    {
        var claim = principal.FindFirst("admin");
        return bool.Parse(claim!.Value);
    }

    public static string UserName(this ClaimsPrincipal principal)
    {
        return principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }
}