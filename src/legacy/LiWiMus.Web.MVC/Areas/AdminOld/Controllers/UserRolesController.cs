using LiWiMus.Core.Roles;
using LiWiMus.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.Areas.AdminOld.Controllers;

[Area("AdminOld")]
[Authorize(Roles = "Admin")]
public class UserRolesController : Controller
{
    private readonly SignInManager<Core.Users.User> _signInManager;
    private readonly UserManager<Core.Users.User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserRolesController(UserManager<Core.Users.User> userManager,
                               SignInManager<Core.Users.User> signInManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index(string userId)
    {
        var viewModel = new List<UserRolesViewModel>();
        var user = await _userManager.FindByIdAsync(userId);

        foreach (var role in _roleManager.Roles.ToList())
        {
            var userRolesViewModel = new UserRolesViewModel
            {
                RoleName = role.Name,
            };
            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                userRolesViewModel.Selected = true;
            }
            else
            {
                userRolesViewModel.Selected = false;
            }

            viewModel.Add(userRolesViewModel);
        }

        var model = new ManageUserRolesViewModel()
        {
            UserId = userId,
            UserRoles = viewModel
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(id);

        var selectedRoles = model.UserRoles
                                 .Where(userRole => userRole.Selected)
                                 .Select(userRole => userRole.RoleName)
                                 .ToList();

        var existingRoles = await _userManager.GetRolesAsync(user);

        var newRoles = selectedRoles.Except(existingRoles).ToList();
        await _userManager.AddToRolesAsync(user, newRoles);

        var removedRoles = existingRoles.Except(selectedRoles).ToList();
        await _userManager.RemoveFromRolesAsync(user, removedRoles);

        return RedirectToAction("Index", "Users", new {area = "AdminOld"});
    }
}