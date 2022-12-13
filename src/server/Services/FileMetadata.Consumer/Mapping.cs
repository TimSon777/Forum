using Domain.Events;

namespace FileMetadata.Consumer;

public static class Mapping
{
    public static FileUploadedEvent Map(this FileMetadataEvent item) => new()
    {
        RequestId = item.RequestId
    };
}