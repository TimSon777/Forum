namespace Forum.API.Data;

public class SendMessageItem
{
    public string Name { get; set; } = "";
    public string Text { get; set; } = "";
    public string? FileKey { get; set; }
}