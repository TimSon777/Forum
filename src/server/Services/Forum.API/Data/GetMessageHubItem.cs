namespace Forum.Handler.Data;

public class GetMessageHubItem
{
    public long IpAddress { get; set; } = default;
    public string Text { get; set; } = default!;
    public string? FileKey { get; set; } = default!;
}