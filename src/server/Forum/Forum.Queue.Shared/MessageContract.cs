namespace Forum.Contracts;

public sealed class MessageContract
{
    public string UserName { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string? FileKey { get; set; }
}