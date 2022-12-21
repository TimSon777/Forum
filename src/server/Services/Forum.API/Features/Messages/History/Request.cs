namespace Forum.API.Features.Messages.History;

public sealed class Request
{
    public string UserName { get; set; } = default!;
    public int CountMessages { get; set; }
}