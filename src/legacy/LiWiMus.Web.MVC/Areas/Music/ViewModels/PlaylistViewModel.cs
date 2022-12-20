using LiWiMus.SharedKernel;
using LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;

namespace LiWiMus.Web.MVC.Areas.Music.ViewModels;

public class PlaylistViewModel : HasId
{
    public UserGeneralInfoViewModel Owner { get; set; } = null!;
    public string Name { get; set; } = null!;

    public bool IsPublic { get; set; }
    public string? PhotoPath { get; set; }

    public List<TrackViewModel> Tracks { get; set; } = null!;
    public int CountSubscribers { get; set; }
    public bool IsOwner { get; set; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string PrefixFiles { get; set; } = "";
    public bool IsSubscribed { get; set; }
}