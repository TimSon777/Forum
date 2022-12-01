namespace Forum.API.Features.History.Responses;

public sealed class GetMessageResponse
{
    public string UserName { get; set; } = default!;
    public string Text { get; set; } = default!;
    public string? FileKey { get; set; } = default!;
}