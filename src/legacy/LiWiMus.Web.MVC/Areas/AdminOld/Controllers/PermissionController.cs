using LiWiMus.Core.Constants;
using LiWiMus.Core.Roles;
using LiWiMus.Web.Areas.Admin.ViewModels;
using LiWiMus.Web.Areas.AdminOld.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.Areas.AdminOld.Controllers;

[Area("AdminOld")]
[Authorize(Roles = "Admin")]
public class PermissionController : Controller
{
    private readonly RoleManager<Role> _roleManager;

    public PermissionController(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<ActionResult> Index(string roleId)
    {
        var allPermissions = Permissions
                             .GetAllPermissions()
                             .Select(permission =>
                                 new RoleClaimsViewModel
                                 {
                                     Type = Permissions.ClaimType,
                                     Value = permission
                                 })
                             .ToList();
        var role = await _roleManager.FindByIdAsync(roleId);
        var claims = await _roleManager.GetClaimsAsync(role);
        var allClaimValues = allPermissions.Select(a => a.Value).ToList();
        var roleClaimValues = claims.Select(a => a.Value).ToList();
        var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
        foreach (var permission in allPermissions)
        {
            permission.Selected = authorizedClaims.Contains(permission.Value);
        }

        var model = new PermissionViewModel
        {
            RoleId = roleId,
            RoleClaims = allPermissions
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(PermissionViewModel model)
    {
        var role = await _roleManager.FindByIdAsync(model.RoleId);
        var claims = await _roleManager.GetClaimsAsync(role);
        foreach (var claim in claims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
        foreach (var claim in selectedClaims)
        {
            await _roleManager.AddPermissionClaim(role, claim.Value);
        }

        return RedirectToAction("Index", "Roles", new {area = "AdminOld"});
    }
}