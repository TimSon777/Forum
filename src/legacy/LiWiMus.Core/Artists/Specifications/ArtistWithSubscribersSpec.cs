using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Artists.Specifications;

public sealed class ArtistWithSubscribersSpec : Specification<Artist>, ISingleResultSpecification
{
    public ArtistWithSubscribersSpec(int id)
    {
        Query
            .Where(artist => artist.Id == id)
            .Include(artist => artist.Subscribers)
            .ThenInclude(likedArtist => likedArtist.User);
    }
}

public static partial class ArtistsRepositoryExtensions
{
    public static async Task<Artist?> WithSubscribers(this IRepository<Artist> repository, int id)
    {
        var spec = new ArtistWithSubscribersSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}