using LiWiMus.Core.Users.Enums;
using LiWiMus.SharedKernel;

namespace LiWiMus.Web.MVC.ViewModels.ForListViewModels;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class UserForListViewModel : HasId
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? Patronymic { get; set; }
    public DateOnly? BirthDate { get; set; }
    public virtual Gender? Gender { get; set; }
    public string? AvatarLocation { get; set; }
    public string UserName { get; set; } = "";
}