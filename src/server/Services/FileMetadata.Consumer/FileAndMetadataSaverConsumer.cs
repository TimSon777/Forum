using Application.Abstractions;
using Domain.Data;
using Domain.Events;
using FileMetadata.Consumer.Exceptions;
using Infrastructure.Abstractions;
using MassTransit;

namespace FileMetadata.Consumer;

public sealed class FileAndMetadataSaverConsumer : IConsumer<FileMetadataEvent>
{
    private readonly ICachingService _cachingService;
    private readonly IMetadataRepository _metadataRepository;
    private readonly IFileMover _fileMover;
    private readonly IBus _bus;

    public FileAndMetadataSaverConsumer(ICachingService cachingService,
        IMetadataRepository metadataRepository,
        IFileMover fileMover,
        IBus bus)
    {
        _cachingService = cachingService;
        _metadataRepository = metadataRepository;
        _fileMover = fileMover;
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<FileMetadataEvent> context)
    {
        var requestId = context.Message.RequestId;
        var increment = await _cachingService.IncrementAsync(requestId);

        if (increment != 2)
        {
            return;
        }

        var fileId = await _cachingService.FindFileIdAsync(requestId);

        if (!fileId.HasValue)
        {
            throw new FileIdNotFoundException();
        }
        
        await SaveMetadataAsync(requestId, fileId.Value);
        await MoveFileToPersistenceBucketAsync(fileId.Value);

        var fileUploadedEvent = context.Message.Map();
        await _bus.Publish(fileUploadedEvent);

    }

    private async Task MoveFileToPersistenceBucketAsync(Guid fileId)
    {
        var isMoved = await _fileMover.MoveToPersistenceAsync(fileId);

        if (!isMoved)
        {
            throw new FileNotMovedToPersistenceBucketException();
        }
    }

    private async Task SaveMetadataAsync(Guid requestId, Guid fileId)
    {
        var metadata = await _cachingService.FindMetadataAsync(requestId);

        if (metadata is null)
        {
            return;
        }

        var item = new SaveMetadataItem
        {
            Metadata = metadata
        };
        
        var isSaved = await _metadataRepository.SaveMetadataAsync(fileId, item);

        if (!isSaved)
        {
            throw new MetadataNotSavedException();
        }
    }
}