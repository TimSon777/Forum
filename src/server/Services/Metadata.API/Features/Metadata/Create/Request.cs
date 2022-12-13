namespace Metadata.API.Features.Metadata.Create;

public sealed class Request
{
    public Guid RequestId { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = default!;
}