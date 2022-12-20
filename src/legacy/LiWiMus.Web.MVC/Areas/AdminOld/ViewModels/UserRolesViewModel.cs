namespace LiWiMus.Web.Areas.Admin.ViewModels;

public class UserRolesViewModel
{
    public string RoleName { get; set; }
    public DateTime GrantedAt { get; set; }
    public DateTime ActiveUntil { get; set; }
    public bool Selected { get; set; }
}