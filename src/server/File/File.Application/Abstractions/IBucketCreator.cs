namespace File.Application.Abstractions;

public interface IBucketCreator
{
    Task CreatePersistenceBucketIfNotExistAsync();
    Task CreateTemporaryBucketIfNotExistAsync();
}