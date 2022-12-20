using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Playlists.Specifications;

public sealed class PlaylistSubscribedByUserIdSpec : Specification<Playlist>
{
    public PlaylistSubscribedByUserIdSpec(int userId)
    {
        Query.Where(p => p.Subscribers.Any(x => x.User.Id == userId));
    }
}

public static partial class PlaylistsRepositoryExtensions
{
    public static async Task<List<Playlist>> SubscribedAsync(this IRepository<Playlist> repository, int userId)
    {
        var spec = new PlaylistSubscribedByUserIdSpec(userId);
        return await repository.ListAsync(spec);
    }
}