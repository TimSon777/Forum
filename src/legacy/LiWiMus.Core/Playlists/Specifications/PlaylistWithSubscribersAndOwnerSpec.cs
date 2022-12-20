using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Playlists.Specifications;

public sealed class PlaylistWithSubscribersAndOwnerSpec : Specification<Playlist>, ISingleResultSpecification
{
    public PlaylistWithSubscribersAndOwnerSpec(int id)
    {
        Query
            .Where(playlist => playlist.Id == id)
            .Include(playlist => playlist.Owner)
            .Include(playlist => playlist.Subscribers)
            .ThenInclude(likedPlaylist => likedPlaylist.User);
    }
}

public static partial class PlaylistsRepositoryExtensions
{
    public static async Task<Playlist?> WithSubscribersAndOwner(this IRepository<Playlist> repository, int id)
    {
        var spec = new PlaylistWithSubscribersAndOwnerSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}