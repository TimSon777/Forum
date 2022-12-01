using Metadata.Domain.Data;

namespace Metadata.Application.Abstractions;

public interface IMetadataProvider
{
    Task SaveMetadataAsync(MetadataItem item, CancellationToken cancellationToken = new());
}