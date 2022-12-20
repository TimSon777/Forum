using Ardalis.Specification;
using LiWiMus.Core.Albums;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Artists.Specifications;

public sealed class ArtistsInAlbumSpec : Specification<Artist>
{
    public ArtistsInAlbumSpec(int albumId)
    {
        Query.Where(artist => artist.Albums.Any(album => album.Id == albumId));
    }

    public ArtistsInAlbumSpec(int albumId, int page, int itemsPerPage)
    {
        Query.Where(artist => artist.Albums.Any(album => album.Id == albumId))
             .Paginate(page, itemsPerPage);
    }
}

public static partial class ArtistsRepositoryExtensions
{
    public static async Task<List<Artist>> ListByAlbumAsync(this IRepository<Artist> repository, Album album, int page,
                                                            int itemsPerPage)
    {
        var spec = new ArtistsInAlbumSpec(album.Id, page, itemsPerPage);
        return await repository.ListAsync(spec);
    }

    public static async Task<List<Artist>> ListByAlbumAsync(this IRepository<Artist> repository, Album album)
    {
        var spec = new ArtistsInAlbumSpec(album.Id);
        return await repository.ListAsync(spec);
    }

    public static async Task<int> CountByAlbumAsync(this IRepository<Artist> repository, Album album)
    {
        var spec = new ArtistsInAlbumSpec(album.Id);
        return await repository.CountAsync(spec);
    }
}