using AutoMapper;
using LiWiMus.Core.Roles;
using LiWiMus.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiWiMus.Web.Areas.AdminOld.Controllers;

[Area("AdminOld")]
[Authorize(Roles ="Admin")]
public class RolesController : Controller
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    public RolesController(RoleManager<Role> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }
    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }

    public IActionResult AddRole()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddRole(RoleViewModel roleViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(roleViewModel);
        }

        var role = _mapper.Map<Role>(roleViewModel);
        await _roleManager.CreateAsync(role);
        return RedirectToAction("Index", "Roles", new {area = "AdminOld"});
    }
}