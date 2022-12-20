using Ardalis.Specification;
using LiWiMus.Core.LikedPlaylists;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Playlists.Specifications;

public sealed class PlaylistCountSubscribersSpec : Specification<Playlist, List<LikedPlaylist>>
{
    public PlaylistCountSubscribersSpec(int id)
    {
        Query
            .Where(p => p.Id == id);

        Query.Select(p => p.Subscribers);
    }
}

public static partial class PlaylistsRepositoryExtensions
{
    public static async Task<int> GetCountSubscribersAsync(this IRepository<Playlist> repository, int id)
    {
        var spec = new PlaylistCountSubscribersSpec(id);
        return await repository.CountAsync(spec);
    }
}