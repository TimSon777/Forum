namespace Forum.API.Features.Messages.History;

public sealed class Response
{
    public string UserName { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string? FileKey { get; set; }
}