using System.Security.Claims;
using LiWiMus.Core.Constants;
using LiWiMus.Core.Roles;
using Microsoft.AspNetCore.Identity;

namespace LiWiMus.Web.Areas.AdminOld.Helpers;

public static class ClaimsHelper
{
    public static async Task AddPermissionClaim(this RoleManager<Role> roleManager, Role role, string permission)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        if (!allClaims.Any(a => a.Type == Permissions.ClaimType && a.Value == permission))
        {
            await roleManager.AddClaimAsync(role, new Claim(Permissions.ClaimType, permission));
        }
    }
}