namespace Broker.Contracts.Forum;

public class GetMessageConsumerItem
{
    public string Text { get; init; } = "";
    public GetUserConsumerItem User { get; init; } = null!;
    public string? FileKey { get; set; }
}