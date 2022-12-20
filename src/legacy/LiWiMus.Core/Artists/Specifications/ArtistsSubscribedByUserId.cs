using Ardalis.Specification;
using LiWiMus.Core.Playlists;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Artists.Specifications;

public sealed class ArtistsSubscribedByUserId : Specification<Artist>
{
    public ArtistsSubscribedByUserId(int userId)
    {
        Query.Where(a => a.Subscribers.Any(s => s.User.Id == userId));
    }
}

public static partial class ArtistsRepositoryExtensions
{
    public static async Task<List<Artist>> SubscribedByUserIdAsync(this IRepository<Artist> repository, int userId)
    {
        var spec = new ArtistsSubscribedByUserId(userId);
        return await repository.ListAsync(spec);
    }
}