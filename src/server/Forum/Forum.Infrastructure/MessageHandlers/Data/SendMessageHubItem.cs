namespace Forum.Infrastructure.MessageHandlers.Data;

public class SendMessageHubItem
{
    public string UserName { get; init; } = default!;
    public string Text { get; init; } = default!;
    public string? FileKey { get; init; }
}