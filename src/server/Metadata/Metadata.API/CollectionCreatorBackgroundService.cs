using MongoDB.Bson;
using MongoDB.Driver;

namespace Metadata.API;

public sealed class CollectionCreatorBackgroundService : BackgroundService
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IHostEnvironment _hostEnvironment;

    public CollectionCreatorBackgroundService(IMongoDatabase mongoDatabase,
        IHostEnvironment hostEnvironment)
    {
        _mongoDatabase = mongoDatabase;
        _hostEnvironment = hostEnvironment;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        await _mongoDatabase.CreateCollectionAsync("metadata", cancellationToken: stoppingToken);
    }
}