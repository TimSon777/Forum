namespace File.API.Features.FileFeature.Requests;

public sealed class SaveFileRequest
{
    public Guid RequestId { get; set; }
    public IFormFile File { get; set; } = default!;
}