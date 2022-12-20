using AutoMapper;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Music.Profiles;

// ReSharper disable once UnusedType.Global
public class ArtisProfile : Profile
{
    public ArtisProfile()
    {
        CreateMap<Core.Artists.Artist, ArtistViewModel>();
    }
}