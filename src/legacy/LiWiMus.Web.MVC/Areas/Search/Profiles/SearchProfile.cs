using AutoMapper;
using LiWiMus.Core.Shared;
using LiWiMus.Web.MVC.Areas.Search.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Search.Profiles;

// ReSharper disable once UnusedType.Global
public class SearchProfile : Profile
{
    public SearchProfile()
    {
        CreateMap<SortViewModel, Sort>();
        CreateMap<SearchViewModel, Pagination>()
            .ForMember(d => d.Sort, opt => opt.MapFrom(s => s.SortViewModel));
    }
}