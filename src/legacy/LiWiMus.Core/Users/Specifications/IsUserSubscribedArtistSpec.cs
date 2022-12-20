using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Users.Specifications;

public sealed class IsUserSubscribedArtistSpec : Specification<User, bool>
{
    public IsUserSubscribedArtistSpec(int id, int artistId)
    {
        Query
            .Where(user => user.Id == id)
            .Include(user => user.LikedArtists)
            .ThenInclude(likedArtist => likedArtist.Artist);

        Query.Select(user => user.LikedArtists.Any(la => la.Artist.Id == artistId));
    }
}

public static partial class ArtistsRepositoryExtensions
{
    public static async Task<bool> IsUserSubscribeArtistAsync(this IRepository<User> repository, int userId, int artistId)
    {
        var spec = new IsUserSubscribedArtistSpec(userId, artistId);
        return await repository.GetBySpecAsync(spec);
    }
}