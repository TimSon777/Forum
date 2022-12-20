using AutoMapper;
using LiWiMus.Core.Users.Enums;
using LiWiMus.Web.MVC.Areas.User.ViewModels;

namespace LiWiMus.Web.MVC.Areas.User.Profiles;

// ReSharper disable once UnusedType.Global
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ProfileViewModel, Core.Users.User>()
            .ForMember(user => user.Gender, opt => opt.MapFrom(vm => vm.IsMale ? Gender.Male : Gender.Female))
            .ReverseMap()
            .ForPath(vm => vm.IsMale, opt => opt.MapFrom(user => user.Gender == Gender.Male));
    }
}