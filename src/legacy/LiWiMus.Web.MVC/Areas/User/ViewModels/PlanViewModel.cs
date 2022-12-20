namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class PlansIndexViewModel
{
    public List<PlanViewModel> ActivePlans { get; set; } = new();
    public List<PlanViewModel> AvailablePlans { get; set; } = new();
}

public class PlanViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public decimal PricePerMonth { get; set; }
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; }

    public List<PermissionViewModel> Permissions { get; set; } = new();
}

public class PermissionViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public class BuyPlanViewModel
{
    public int PlanId { get; set; }
    public int MonthsNumber { get; set; }
}