using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Tracks.Specifications;

public sealed class TracksSubscribedByUserIdSpec : Specification<Track>
{
    public TracksSubscribedByUserIdSpec(int userId)
    {
        Query.Where(t => t.Subscribers.Any(x => x.User.Id == userId));
    }
}

public static partial class TracksRepositoryExtensions
{
    public static async Task<List<Track>> SubscribedAsync(this IRepository<Track> repository, int userId)
    {
        var spec = new TracksSubscribedByUserIdSpec(userId);
        return await repository.ListAsync(spec);
    }
}