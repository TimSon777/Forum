using AutoMapper;
using LiWiMus.Web.MVC.Areas.Artist.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Artist.Profiles;

public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        CreateMap<CreateArtistViewModel, Core.Artists.Artist>().ReverseMap();
        CreateMap<UpdateArtistViewModel, Core.Artists.Artist>().ReverseMap();
    }
}