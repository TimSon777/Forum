namespace Forum.Domain.Entities;

public sealed class Message
{
    public string Text { get; set; } = default!;
    public string? FileKey { get; set; } = default!;
    public User User { get; set; } = default!;
}