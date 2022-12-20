using AutoMapper;
using LiWiMus.Core.Plans;
using LiWiMus.Web.MVC.Areas.User.ViewModels;

namespace LiWiMus.Web.MVC.Areas.User.Profiles;

public class PlansProfile : Profile
{
    public PlansProfile()
    {
        CreateMap<Plan, PlanViewModel>();
        CreateMap<Permission, PermissionViewModel>();
    }
}