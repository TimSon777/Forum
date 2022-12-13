namespace FileMetadata.Consumer.Exceptions;

public sealed class FileNotMovedToPersistenceBucketException : Exception
{
    public FileNotMovedToPersistenceBucketException()
        : base("File not moved to persistence bucket")
    { }
}