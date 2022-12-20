using AutoMapper;
using LiWiMus.Core.Offices;

namespace LiWiMus.Web.MVC.Areas.Company;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Office, OfficeDto>();
    }
}