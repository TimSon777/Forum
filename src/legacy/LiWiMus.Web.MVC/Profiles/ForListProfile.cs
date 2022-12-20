using AutoMapper;
using LiWiMus.Core.Albums;
using LiWiMus.Core.Genres;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.Tracks;
using LiWiMus.Web.MVC.ViewModels.ForListViewModels;

namespace LiWiMus.Web.MVC.Profiles;

// ReSharper disable once UnusedType.Global
public class ForListProfile : Profile
{
    public ForListProfile()
    {
        CreateMap<Playlist, PlaylistForListViewModel>();
        CreateMap<Album, AlbumForListViewModel>();
        CreateMap<Track, TrackForListViewModel>();
        CreateMap<Core.Artists.Artist, ArtistForListViewModel>();
        CreateMap<Genre, GenreForListViewModel>();
        CreateMap<Core.Users.User, UserForListViewModel>();
    }
}