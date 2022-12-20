using LiWiMus.SharedKernel;
using LiWiMus.Web.MVC.ViewModels.ForListViewModels;
using LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;

namespace LiWiMus.Web.MVC.Areas.Music.ViewModels;

public class AlbumViewModel : HasId
{
    public string Title { get; set; } = null!;

    public DateOnly PublishedAt { get; set; }
    public string CoverLocation { get; set; } = null!;

    public List<ArtistGeneralInfoViewModel> Owners { get; set; } = new();
    public List<TrackForListViewModel> Tracks { get; set; } = new();
    public bool IsSubscribed { get; set; }
}