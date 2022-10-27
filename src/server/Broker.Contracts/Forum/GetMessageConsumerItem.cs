namespace Broker.Contracts.Forum;

public class GetMessageConsumerItem
{
    public string Text { get; init; } = "";
    public GetUserConsumerItem User { get; init; } = null!;
    public Guid? FileKey { get; set; }
}