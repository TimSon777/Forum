namespace FileMetadata.Consumer.Exceptions;

public sealed class MetadataNotSavedException : Exception
{
    public MetadataNotSavedException()
        : base("Metadata not saved")
    { }
}