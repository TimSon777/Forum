using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Albums.Specifications;

public sealed class DetailedAlbumByIdSpec : Specification<Album>, ISingleResultSpecification
{
    public DetailedAlbumByIdSpec(int albumId, bool withSubscribers = false)
    {
        Query.Where(album => album.Id == albumId)
             .Include(album => album.Tracks)
             .Include(album => album.Owners);

        if (withSubscribers)
        {
            Query
                .Include(album => album.Subscribers)
                .ThenInclude(la => la.User);
        }
    }
}

public static partial class AlbumRepositoryExtensions
{
    public static async Task<Album?> GetDetailedAlbumAsync(this IRepository<Album> repository, int id, bool withSubscribers = false)
    {
        var spec = new DetailedAlbumByIdSpec(id, withSubscribers);
        return await repository.GetBySpecAsync(spec);
    }
}