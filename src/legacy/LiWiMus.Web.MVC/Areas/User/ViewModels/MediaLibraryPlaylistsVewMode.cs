using LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;

namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class MediaLibraryPlaylistsVewMode
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public List<PlaylistGeneralInfoViewModel> Playlists { get; set; } = null!;
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public List<PlaylistGeneralInfoViewModel> SubscribedPlaylists { get; set; } = null!;
}