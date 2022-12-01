using Cache;
using EasyCaching.Core;
using FileMetadata.Queue.Shared;
using MassTransit;
using Metadata.Application.Abstractions;
using Metadata.Domain.Data;
using Newtonsoft.Json;

namespace Metadata.Infrastructure.Implementations;

public sealed class MetadataProvider : IMetadataProvider
{
    private readonly IRedisCachingProvider _cache;
    private readonly IBus _bus;

    public MetadataProvider(IRedisCachingProvider cache, IBus bus)
    {
        _cache = cache;
        _bus = bus;
    }

    public async Task SaveMetadataAsync(MetadataItem item, CancellationToken cancellationToken = new())
    {
        var json = JsonConvert.SerializeObject(item.Metadata);
        await _cache.HSetAsync(item.RequestId.ToString(), FileAndMetadataCacheConstants.MetadataField, json);

        var contract = new FileAndMetadataContract
        {
            RequestId = item.RequestId
        };

        await _bus.Publish(contract, cancellationToken);
    }
}