using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Albums.Specifications;

public sealed class AlbumWithSubscribersSpec : Specification<Album>, ISingleResultSpecification
{
    public AlbumWithSubscribersSpec(int id)
    {
        Query
            .Where(album => album.Id == id)
            .Include(album => album.Subscribers)
            .ThenInclude(la => la.User);
    }
}

public static partial class AlbumRepositoryExtensions
{
    public static async Task<Album?> GetAlbumWithSubscribersAsync(this IRepository<Album> repository, int id)
    {
        var spec = new AlbumWithSubscribersSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}