namespace Chat.Infrastructure.MessageHandlers.Data;

public class GetMessageConsumerItem
{
    public string Text { get; set; } = "";
    public GetUserConsumerItem User { get; set; } = null!;
}