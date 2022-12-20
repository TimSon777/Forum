using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Albums.Specifications;

public sealed class AlbumWithArtistsSpec : Specification<Album>, ISingleResultSpecification
{
    public AlbumWithArtistsSpec(int id)
    {
        Query
            .Where(album => album.Id == id)
            .Include(album => album.Owners);
    }
}

public static partial class AlbumRepositoryExtensions
{
    public static async Task<Album?> GetAlbumWithOwnersAsync(this IRepository<Album> repository, int id)
    {
        var spec = new AlbumWithArtistsSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
} 