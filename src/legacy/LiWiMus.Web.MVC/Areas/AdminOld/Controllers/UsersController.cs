using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiWiMus.Web.Areas.AdminOld.Controllers;

[Area("AdminOld")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<Core.Users.User> _userManager;
    public UsersController(UserManager<Core.Users.User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(HttpContext.User);
        var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
        return View(allUsersExceptCurrentUser);
    }
}