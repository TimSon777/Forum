using AutoMapper;
using LiWiMus.Core.Albums;
using LiWiMus.Core.Artists;
using LiWiMus.Core.Genres;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.Users;
using LiWiMus.Web.MVC.ViewModels;
using LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;

namespace LiWiMus.Web.MVC.Profiles;

// ReSharper disable once UnusedType.Global
public class GeneralInfoProfile : Profile
{
    public GeneralInfoProfile()
    {
        CreateMap<Genre, GenreGeneralInfoViewModel>();
        CreateMap<Album, AlbumGeneralInfoViewModel>();
        CreateMap<User, UserGeneralInfoViewModel>();
        CreateMap<Artist, ArtistGeneralInfoViewModel>();
        CreateMap<Playlist, PlaylistGeneralInfoViewModel>();
    }
}