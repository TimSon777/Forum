using System.Text.Json;
using Cache;
using EasyCaching.Core;
using FileMetadata.Queue.Listener.Abstractions;
using FileMetadata.Queue.Listener.Exceptions;
using FileMetadata.Queue.Shared;
using MassTransit;
using MongoDB.Bson;

namespace FileMetadata.Queue.Listener;

public sealed class FileAndMetadataSaverConsumer : IConsumer<FileAndMetadataContract>
{
    private readonly IRedisCachingProvider _cache;
    private readonly IMetadataRepository _metadataRepository;
    private readonly IFileMover _fileMover;

    public FileAndMetadataSaverConsumer(IRedisCachingProvider cache,
        IMetadataRepository metadataRepository,
        IFileMover fileMover)
    {
        _cache = cache;
        _metadataRepository = metadataRepository;
        _fileMover = fileMover;
    }

    public async Task Consume(ConsumeContext<FileAndMetadataContract> context)
    {
        var requestId = context.Message.RequestId.ToString();
        var incrementInStr = await _cache.HGetAsync(requestId, FileAndMetadataCacheConstants.IncrementField);

        if (incrementInStr is null)
        {
            return;
        }

        var increment = int.Parse(incrementInStr);

        if (increment != 2)
        {
            return;
        }

        await SaveMetadataAsync(requestId);
        await MoveFileToPersistenceBucketAsync(requestId);
    }

    private async Task MoveFileToPersistenceBucketAsync(string requestId)
    {
        var isMoved = await _fileMover.MoveToPersistenceAsync(requestId);

        if (!isMoved)
        {
            throw new FileNotMovedToPersistenceBucketException();
        }
    }

    private async Task SaveMetadataAsync(string requestId)
    {
        var metadataJson = await _cache.HGetAsync(requestId, FileAndMetadataCacheConstants.MetadataField);

        var metadata = JsonSerializer.Deserialize<Dictionary<string, string>>(metadataJson);

        if (metadata is null)
        {
            return;
        }
        
        var isSaved = await _metadataRepository.SaveAsync(new BsonDocument(metadata));

        if (!isSaved)
        {
            throw new MetadataNotSavedException();
        }
    }
}