namespace FileMetadata.Queue.Listener.Abstractions;

public interface IFileMover
{
    Task<bool> MoveToPersistenceAsync(string fileKey);
}