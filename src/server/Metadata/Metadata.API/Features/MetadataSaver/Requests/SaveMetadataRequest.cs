namespace Metadata.API.Features.MetadataSaver.Requests;

public sealed class SaveMetadataRequest
{
    public Guid RequestId { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = default!;
}