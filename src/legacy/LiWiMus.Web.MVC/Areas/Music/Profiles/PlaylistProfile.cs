using AutoMapper;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.PlaylistTracks;
using LiWiMus.Core.Tracks;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Music.Profiles;

// ReSharper disable once UnusedType.Global
public class PlaylistProfile : Profile
{
    public PlaylistProfile()
    {
        CreateMap<Track, TrackViewModel>();
        CreateMap<Playlist, PlaylistViewModel>();
    }
}