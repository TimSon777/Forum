using MongoDB.Bson;

namespace FileMetadata.Queue.Listener.Abstractions;

public interface IMetadataRepository
{
    Task<bool> SaveAsync(BsonDocument document);
}