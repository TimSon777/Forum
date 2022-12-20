using LiWiMus.Web.MVC.ViewModels.ForListViewModels;

namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class TracksPlaylistViewModel
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public List<TrackForListViewModel> Tracks { get; set; } = new();
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public int PlaylistId { get; set; }
}