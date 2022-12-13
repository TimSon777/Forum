namespace Domain.Entities;

public sealed class Message
{
    public string Text { get; set; } = default!;
    public string? FileKey { get; set; }
    public User User { get; set; } = default!;
}