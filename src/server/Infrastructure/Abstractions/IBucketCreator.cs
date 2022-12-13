namespace Infrastructure.Abstractions;

public interface IBucketCreator
{
    Task CreatePersistenceBucketIfNotExistAsync();
    Task CreateTemporaryBucketIfNotExistAsync();
}