using Ardalis.Specification;
using LiWiMus.Core.Tracks.Specifications;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Playlists.Specifications;

public sealed class IsUserSubscribedToPlaylistSpec : Specification<Playlist, bool>
{
    public IsUserSubscribedToPlaylistSpec(int userId, int playlistId)
    {
        Query.Where(t => t.Id == userId);
        Query.Select(t => t.Subscribers.Any(x => x.Playlist.Id == playlistId));
    }
}

public static partial class PlaylistsRepositoryExtensions
{
    public static async Task<bool> IsUserSubscribedAsync(this IRepository<Playlist> repository, int userId, int playlistId)
    {
        var spec = new IsUserSubscribedToPlaylistSpec(userId, playlistId);
        return await repository.GetBySpecAsync(spec);
    }
}