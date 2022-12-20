using LiWiMus.Core.Chats.Enums;
using LiWiMus.Core.Messages;

namespace LiWiMus.Core.Chats;

public class Chat : BaseEntity
{
    public User User { get; set; } = null!;

    public ChatStatus Status { get; set; } = ChatStatus.Opened;
    
    public string UserConnectionId { get; set; } = "";

    public string? ConsultantConnectionId { get; set; } = "";

    public List<Message> Messages { get; set; } = new();
}