namespace Chat.DAL.Abstractions.Chat;

public class AddMessageItem
{
    public AddUserItem User { get; set; } = null!;
    public string Text { get; set; } = "";
}