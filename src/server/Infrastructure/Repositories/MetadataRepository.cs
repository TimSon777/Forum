using Application.Abstractions;
using Domain.Data;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public sealed class MetadataRepository : IMetadataRepository
{
    private readonly IMongoCollection<BsonDocument> _collection;
    private readonly ILogger<MetadataRepository> _logger;

    public MetadataRepository(IMongoDatabase database, ILogger<MetadataRepository> logger)
    {
        _logger = logger;
        _collection = database.GetCollection<BsonDocument>("metadata");
    }

    public async Task<bool> SaveMetadataAsync(Guid fileId, SaveMetadataItem item, CancellationToken cancellationToken = new())
    {
        var document = new BsonDocument(item.Metadata)
        {
            {"fileId", fileId}
        };

        try
        {
            await _collection.InsertOneAsync(document, cancellationToken: cancellationToken);
            return true;
        }
        catch (MongoException ex)
        {
            _logger.LogException(ex);
            return false;
        }
    }
}
