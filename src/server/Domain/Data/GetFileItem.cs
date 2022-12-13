namespace Domain.Data;

public sealed class GetFileItem
{
    public required Stream File { get; set; } = default!;
    public required string ContentType { get; set; } = default!;
}