using Domain.Data;

namespace Application.Abstractions;

public interface IMetadataRepository
{
    Task<bool> SaveMetadataAsync(Guid fileId, SaveMetadataItem item, CancellationToken cancellationToken = new());
}