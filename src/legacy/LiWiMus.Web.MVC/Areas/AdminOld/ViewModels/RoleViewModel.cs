namespace LiWiMus.Web.Areas.Admin.ViewModels;

public class RoleViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPublic { get; set; }
    public decimal? PricePerMonth { get; set; }
    public TimeSpan DefaultTimeout { get; set; }
}