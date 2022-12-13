using MongoDB.Driver;

namespace Metadata.API;

public sealed class CollectionCreatorBackgroundService : BackgroundService
{
    private readonly IMongoDatabase _mongoDatabase;

    public CollectionCreatorBackgroundService(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        await _mongoDatabase.CreateCollectionAsync("metadata", cancellationToken: stoppingToken);
    }
}