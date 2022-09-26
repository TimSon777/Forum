namespace Chat.DAL.Abstractions.Chat;

// ReSharper disable once ClassNeverInstantiated.Global
public class GetMessageItem
{
    public GetUserItem User { get; set; } = null!;
    public string Text { get; set; } = "";
}