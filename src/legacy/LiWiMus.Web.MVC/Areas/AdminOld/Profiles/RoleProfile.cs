using AutoMapper;
using LiWiMus.Core.Roles;
using LiWiMus.Web.Areas.Admin.ViewModels;

namespace LiWiMus.Web.Areas.AdminOld.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleViewModel, Role>();
    }
}