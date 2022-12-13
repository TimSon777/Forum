namespace File.API.Features.File.Create;

public sealed class Request
{
    public Guid RequestId { get; set; }
    public IFormFile File { get; set; } = default!;
}