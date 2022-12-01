using File.Application.Abstractions;

namespace File.API;

public sealed class BucketCreatorBackgroundService : BackgroundService
{
    private readonly IBucketCreator _bucketCreator;

    public BucketCreatorBackgroundService(IBucketCreator bucketCreator)
    {
        _bucketCreator = bucketCreator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        await _bucketCreator.CreatePersistenceBucketIfNotExistAsync();
        await _bucketCreator.CreateTemporaryBucketIfNotExistAsync();
    }
}