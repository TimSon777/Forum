namespace FileMetadata.Queue.Listener.Exceptions;

public sealed class MetadataNotSavedException : Exception
{
    public MetadataNotSavedException()
        : base("Metadata not saved")
    { }
}