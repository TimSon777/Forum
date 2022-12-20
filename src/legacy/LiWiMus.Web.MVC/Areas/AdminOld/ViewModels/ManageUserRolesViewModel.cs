namespace LiWiMus.Web.Areas.Admin.ViewModels;

public class ManageUserRolesViewModel
{
    public string UserId { get; set; }
    public IList<UserRolesViewModel> UserRoles { get; set; }
}