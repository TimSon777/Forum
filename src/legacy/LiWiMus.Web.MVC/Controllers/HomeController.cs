using LiWiMus.Web.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.MVC.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("[action]/{code:int}")]
    public IActionResult Errors(int code)
    {
        var model = new ErrorViewModel {StatusCode = code};
        return View(model);
    }

    [HttpGet("[action]")]
    public IActionResult Error()
    {
        return View();
    }
}