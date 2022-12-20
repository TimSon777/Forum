using AutoMapper;
using LiWiMus.Core.Tracks;
using LiWiMus.Web.MVC.Areas.Artist.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Artist.Profiles;

public class TrackProfile : Profile
{
    public TrackProfile()
    {
        CreateMap<CreateTrackViewModel, Track>().ReverseMap();
        CreateMap<UpdateTrackViewModel, Track>().ReverseMap();
    }
}