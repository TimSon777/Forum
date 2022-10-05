namespace Chat.Infrastructure.MessageHandlers.Data;

// ReSharper disable once ClassNeverInstantiated.Global
public class GetMessageHubItem
{
    // ReSharper disable once InconsistentNaming
    public int IPv4 { get; set; } = default;
    public int Port { get; set; } = default;
    public string Text { get; set; } = "";
}