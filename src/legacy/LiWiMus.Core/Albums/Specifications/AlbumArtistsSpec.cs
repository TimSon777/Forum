using Ardalis.Specification;
using LiWiMus.Core.Artists;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Albums.Specifications;

public sealed class AlbumArtistsSpec : Specification<Album, ICollection<Artist>>
{
    public AlbumArtistsSpec(int albumId)
    {
        Query.Where(album => album.Id == albumId);
        Query.Select(album => album.Owners);
    }
}

public static partial class AlbumRepositoryExtensions
{
    public static async Task<ICollection<Artist>> GetArtistsAsync(this IRepository<Album> repository, Album album)
    {
        var albumArtistsSpec = new AlbumArtistsSpec(album.Id);
        return await repository.GetBySpecAsync(albumArtistsSpec) ?? new List<Artist>();
    }
}