using LiWiMus.SharedKernel;
using LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;

namespace LiWiMus.Web.MVC.ViewModels.ForListViewModels;

public class PlaylistForListViewModel : HasId
{
    public UserGeneralInfoViewModel Owner { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? PhotoLocation { get; set; }
    public int CountSubscribers { get; set; }
    public bool IsOwner { get; set; }
}