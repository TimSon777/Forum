using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Tracks.Specifications;


public sealed class IsUserSubscribeTrackSpec : Specification<User, bool>
{
    public IsUserSubscribeTrackSpec(int userId, int trackId)
    {
        Query.Where(t => t.Id == userId);
        Query.Select(t => t.LikedSongs.Any(x => x.Track.Id == trackId));
    }
}

public static partial class TracksRepositoryExtensions
{
    public static async Task<bool> IsUserSubscribeAsync(this IRepository<User> repository, int userId, int trackId)
    {
        var spec = new IsUserSubscribeTrackSpec(userId, trackId);
        return await repository.GetBySpecAsync(spec);
    }
}