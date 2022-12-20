namespace LiWiMus.Web.Areas.Admin.ViewModels;

public class PermissionViewModel
{
    public string RoleId { get; set; }
    public IList<RoleClaimsViewModel> RoleClaims { get; set; }
}