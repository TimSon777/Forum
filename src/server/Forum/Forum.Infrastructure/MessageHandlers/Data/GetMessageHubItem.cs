namespace Forum.Infrastructure.MessageHandlers.Data;

public class GetMessageHubItem
{
    // ReSharper disable once InconsistentNaming
    public long IpAddress { get; set; } = default;
    public string Text { get; set; } = default!;
    public string? FileKey { get; set; } = default!;
}