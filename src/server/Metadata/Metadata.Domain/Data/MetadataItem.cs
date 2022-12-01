namespace Metadata.Domain.Data;

public sealed class MetadataItem
{
    public Guid RequestId { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = default!;
}