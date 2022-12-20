using AutoMapper;
using LiWiMus.Core.Genres;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Music.Profiles;

// ReSharper disable once UnusedType.Global
public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreViewModel>();
    }
}