using Metadata.API.Features.MetadataSaver.Requests;
using Metadata.Domain.Data;
using Riok.Mapperly.Abstractions;

namespace Metadata.API.Features.MetadataSaver;

[Mapper]
public static partial class MetadataMapper
{
    public static partial MetadataItem Map(this SaveMetadataRequest request);
}