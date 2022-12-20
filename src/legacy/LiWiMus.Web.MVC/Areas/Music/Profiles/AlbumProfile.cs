using AutoMapper;
using LiWiMus.Core.Albums;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Music.Profiles;

// ReSharper disable once UnusedType.Global
public class AlbumProfile : Profile
{
    public AlbumProfile()
    {
        CreateMap<Album, AlbumViewModel>();
    }
}