using AutoMapper;
using LiWiMus.Core.Chats;
using LiWiMus.Core.Messages;
using LiWiMus.Web.MVC.Areas.User.ViewModels;

namespace LiWiMus.Web.MVC.Areas.User.Profiles;

// ReSharper disable once UnusedType.Global
public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<Core.Users.User, UserChatViewModel>().ReverseMap();
        CreateMap<Message, MessageViewModel>().ReverseMap();
        CreateMap<Chat, ChatViewModel>().ReverseMap();
    }
}