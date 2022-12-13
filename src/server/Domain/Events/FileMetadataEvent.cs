namespace Domain.Events;

public sealed class FileMetadataEvent
{
    public Guid RequestId { get; set; }
}