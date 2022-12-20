using AutoMapper;
using LiWiMus.Core.Albums;
using LiWiMus.Web.MVC.Areas.Artist.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Artist.Profiles;

public class AlbumProfile : Profile
{
    public AlbumProfile()
    {
        CreateMap<CreateAlbumViewModel, Album>().ReverseMap();
        CreateMap<UpdateAlbumViewModel, Album>().ReverseMap();
    }
}