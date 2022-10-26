namespace Forum.Infrastructure.MessageHandlers.Data;

public class SendMessageHubItem
{
    public string Name { get; init; } = default!;
    public string Text { get; init; } = default!;
}