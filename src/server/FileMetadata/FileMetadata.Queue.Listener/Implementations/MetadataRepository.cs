using FileMetadata.Queue.Listener.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileMetadata.Queue.Listener.Implementations;

public sealed class MetadataRepository : IMetadataRepository
{
    private readonly IMongoCollection<BsonDocument> _collection;

    public MetadataRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<BsonDocument>("metadata");
    }

    public async Task<bool> SaveAsync(BsonDocument document)
    {
        try
        {
            await _collection.InsertOneAsync(document);
            return true;

        }
        catch (MongoWriteException)
        {
            return false;
        }
    }
}