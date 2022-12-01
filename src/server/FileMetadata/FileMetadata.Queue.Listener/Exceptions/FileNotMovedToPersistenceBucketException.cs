namespace FileMetadata.Queue.Listener.Exceptions;

public sealed class FileNotMovedToPersistenceBucketException : Exception
{
    public FileNotMovedToPersistenceBucketException()
        : base("File not moved to persistence bucket")
    { }
}