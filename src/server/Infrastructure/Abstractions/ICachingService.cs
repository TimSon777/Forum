namespace Infrastructure.Abstractions;

public interface ICachingService
{
    Task SaveMetadataAsync(Guid requestId, Dictionary<string, string> metadata);
    Task<long> IncrementAsync(Guid requestId);
    Task SaveConnectionIdAsync(Guid requestId, string connectionId);
    Task SaveFileIdAsync(Guid requestId, Guid fileId);

    Task<Dictionary<string, string>?> FindMetadataAsync(Guid requestId);
    Task<string?> FindConnectionIdAsync(Guid requestId);
    Task<Guid?> FindFileIdAsync(Guid requestId);
}