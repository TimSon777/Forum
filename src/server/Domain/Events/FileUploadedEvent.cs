namespace Domain.Events;

public sealed class FileUploadedEvent
{
    public Guid RequestId { get; set; }
}