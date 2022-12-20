using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Playlists.Specifications;

public sealed class PlaylistWithTracksAndSubscribersSpec : Specification<Playlist>, ISingleResultSpecification
{
    public PlaylistWithTracksAndSubscribersSpec(int id)
    {
        Query.Where(playlist => playlist.Id == id)
             .Include(playlist => playlist.Tracks)
             .Include(playlist => playlist.Subscribers);
    }
}

public static partial class PlaylistsRepositoryExtensions
{
    public static async Task<Playlist?> GetWithTracksAndSubscribersAsync(this IRepository<Playlist> repository, int id)
    {
        var spec = new PlaylistWithTracksAndSubscribersSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}