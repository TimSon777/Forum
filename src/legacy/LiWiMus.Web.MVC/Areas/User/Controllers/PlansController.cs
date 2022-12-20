using AutoMapper;
using FormHelper;
using LiWiMus.Core.Plans;
using LiWiMus.Core.Plans.Exceptions;
using LiWiMus.Core.Plans.Interfaces;
using LiWiMus.Web.MVC.Areas.User.ViewModels;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LiWiMus.Web.MVC.Areas.User.Controllers;

[Area(AreasConstants.User)]
public class PlansController : Controller
{
    private readonly IPlanManager _planManager;
    private readonly UserManager<Core.Users.User> _userManager;
    private readonly IMapper _mapper;
    private readonly IUserPlanManager _userPlanManager;
    private readonly SignInManager<Core.Users.User> _signInManager;

    public PlansController(IPlanManager planManager, UserManager<Core.Users.User> userManager, IMapper mapper,
                           IUserPlanManager userPlanManager, SignInManager<Core.Users.User> signInManager)
    {
        _planManager = planManager;
        _userManager = userManager;
        _mapper = mapper;
        _userPlanManager = userPlanManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        var allPlans = await _planManager.GetAllAsync();
        var activePlans = await _planManager.GetByUserAsync(user);
        var availablePlans = allPlans.Where(p => activePlans.All(a => a.Name != p.Name));

        var activePlansModel = _mapper.MapList<Plan, PlanViewModel>(activePlans).ToList();
        var availablePlansModel = _mapper.MapList<Plan, PlanViewModel>(availablePlans).ToList();
        var model = new PlansIndexViewModel
        {
            ActivePlans = activePlansModel,
            AvailablePlans = availablePlansModel
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int planId)
    {
        var plan = await _planManager.GetByIdAsync(planId);

        if (plan is null)
        {
            return NotFound();
        }

        var model = _mapper.Map<PlanViewModel>(plan);

        var user = await _userManager.GetUserAsync(User);

        model.IsActive = await _userPlanManager.IsInPlanAsync(user, plan);
        return View(model);
    }

    [HttpPost]
    [FormValidator]
    public async Task<IActionResult> BuyPlan(BuyPlanViewModel model)
    {
        var plan = await _planManager.GetByIdAsync(model.PlanId);

        if (plan is null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);

        try
        {
            await _userPlanManager.BuyPlanAsync(user, plan, TimeSpan.FromDays(30 * model.MonthsNumber));
            await _signInManager.RefreshSignInAsync(user);
            return FormResult.CreateSuccessResult("Ok");
        }
        catch (UserDoesntHaveEnoughMoneyException)
        {
            return FormResult.CreateErrorResult("You don't have enough funds");
        }
        catch (UserAlreadyOwnsPlanException)
        {
            return FormResult.CreateErrorResult("You already own this plan");
        }
    }
}