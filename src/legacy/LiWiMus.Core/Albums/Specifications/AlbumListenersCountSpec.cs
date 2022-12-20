using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Albums.Specifications;

public sealed class AlbumListenersCountSpec : Specification<Album, int>
{
    public AlbumListenersCountSpec(int albumId)
    {
        Query.Where(album => album.Id == albumId);
        Query.Select(album => album.Subscribers.Count);
    }
}

public static partial class AlbumRepositoryExtensions
{
    public static async Task<int> GetListenersCountAsync(this IRepository<Album> repository, Album album)
    {
        var albumListenersCountSpec = new AlbumListenersCountSpec(album.Id);
        return await repository.GetBySpecAsync(albumListenersCountSpec);
    }
}