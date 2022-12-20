using LiWiMus.SharedKernel;
using LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;

namespace LiWiMus.Web.MVC.Areas.Music.ViewModels;

public class ArtistViewModel : HasId
{
    public List<UserGeneralInfoViewModel> Owners { get; set; } = new();
    public string Name { get; set; } = null!;
    public string About { get; set; } = null!;
    public string PhotoLocation { get; set; } = null!;
    public List<AlbumGeneralInfoViewModel> Albums { get; set; } = new();
    public bool IsSubscribed { get; set; }
}