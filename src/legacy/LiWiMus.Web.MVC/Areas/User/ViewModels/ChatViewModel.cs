using LiWiMus.Core.Chats.Enums;

namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class ChatViewModel
{
    public UserChatViewModel User { get; set; } = null!;

    // ReSharper disable once CollectionNeverUpdated.Global
    public List<UserChatViewModel> Consultants { get; set; } = new();

    public string UserConnectionId { get; set; } = "";
    public string ConsultantConnectionId { get; set; } = "";
    public ChatStatus Status { get; set; }

    // ReSharper disable once CollectionNeverUpdated.Global
    public List<MessageViewModel> Messages { get; set; } = new();
}