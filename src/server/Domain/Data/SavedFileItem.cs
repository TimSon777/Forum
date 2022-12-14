namespace Domain.Data;

public sealed class SavedFileItem
{
    public required Guid FileKey { get; set; } = default!;
}