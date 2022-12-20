using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Albums.Specifications;

public sealed class AlbumTracksCountSpec : Specification<Album, int>
{
    public AlbumTracksCountSpec(int albumId)
    {
        Query.Where(album => album.Id == albumId);
        Query.Select(album => album.Tracks.Count);
    }
}

public static partial class AlbumRepositoryExtensions
{
    public static async Task<int> GetTracksCountAsync(this IRepository<Album> repository, Album album)
    {
        var albumTracksCountSpec = new AlbumTracksCountSpec(album.Id);
        return await repository.GetBySpecAsync(albumTracksCountSpec);
    }
}