using LiWiMus.Core.Chats;

namespace LiWiMus.Core.Messages;

public class Message : BaseEntity
{
    public Chat Chat { get; set; } = null!;
    public User? Owner { get; set; }
    public string Text { get; set; } = null!;
}