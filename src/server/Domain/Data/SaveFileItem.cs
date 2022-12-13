namespace Domain.Data;

public sealed class SaveFileItem
{
    public required Stream File { get; set; }
    public required string ContentType { get; set; } = default!;
}