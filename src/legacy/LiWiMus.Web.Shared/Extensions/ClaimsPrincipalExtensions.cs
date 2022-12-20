using System.Security.Claims;
using LiWiMus.SharedKernel.Extensions;

namespace LiWiMus.Web.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int? GetId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToInt();
    }
}