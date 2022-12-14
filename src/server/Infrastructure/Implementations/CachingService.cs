using System.Text.Json;
using Infrastructure.Abstractions;
using StackExchange.Redis;

namespace Infrastructure.Implementations;

public sealed class CachingService : ICachingService
{
    private const string FileIdField = "File Id";
    private const string MetadataField = "Metadata";
    private const string IncrementField = "Increment";
    private const string ConnectionIdField = "Connection Id";
    
    private readonly IDatabase _caching;

    public CachingService(IDatabase caching)
    {
        _caching = caching;
    }
    
    public async Task SaveMetadataAsync(Guid requestId, Dictionary<string, string> metadata)
    {
        var json = JsonSerializer.Serialize(metadata);
        await _caching.HashSetAsync(requestId.ToString(), MetadataField, json);
    }

    public async Task<long> IncrementAsync(Guid requestId)
    {
        return await _caching.HashIncrementAsync(requestId.ToString(), IncrementField);
    }

    public async Task SaveConnectionIdAsync(Guid requestId, string connectionId)
    {
        await _caching.HashSetAsync(requestId.ToString(), ConnectionIdField, connectionId);
    }

    public async Task SaveFileIdAsync(Guid requestId, Guid fileId)
    {
        await _caching.HashSetAsync(requestId.ToString(), FileIdField, fileId.ToString());
    }

    public async Task<Dictionary<string, string>?> FindMetadataAsync(Guid requestId)
    {
        var json = await _caching.HashGetAsync(requestId.ToString(), MetadataField);

        return json.HasValue
            ? JsonSerializer.Deserialize<Dictionary<string, string>>(json.ToString())
            : null;
    }

    public async Task<string?> FindConnectionIdAsync(Guid requestId)
    {
        return await _caching.HashGetAsync(requestId.ToString(), ConnectionIdField);
    }

    public async Task<Guid?> FindFileIdAsync(Guid requestId)
    {
        var fileId = await _caching.HashGetAsync(requestId.ToString(), FileIdField);
        
        return fileId.HasValue
            ? Guid.Parse(fileId.ToString())
            : null;
    }
}