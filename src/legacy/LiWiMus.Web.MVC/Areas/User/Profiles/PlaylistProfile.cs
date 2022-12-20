using AutoMapper;
using LiWiMus.Core.Playlists;
using LiWiMus.Web.MVC.Areas.User.ViewModels;

namespace LiWiMus.Web.MVC.Areas.User.Profiles;

// ReSharper disable once UnusedType.Global
public class PlaylistProfile : Profile
{
    public PlaylistProfile()
    {
        CreateMap<CreatePlaylistViewModel, Playlist>();
    }
}