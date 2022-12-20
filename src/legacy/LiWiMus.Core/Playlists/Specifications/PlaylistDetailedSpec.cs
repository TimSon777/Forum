using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Playlists.Specifications;

public sealed class PlaylistDetailedSpec : Specification<Playlist>, ISingleResultSpecification
{
    public PlaylistDetailedSpec(int id)
    {
        Query
            .Where(playlist => playlist.Id == id)
            .Include(playlist => playlist.Owner)
            .Include(playlist => playlist.Tracks)
            .ThenInclude(track => track.Album)
            .Include(playlist => playlist.Tracks)
            .ThenInclude(track => track.Owners);
    }
}

public static partial class PlaylistsRepositoryExtensions
{
    public static async Task<Playlist?> GetPlaylistDetailedAsync(this IRepository<Playlist> repository, int id)
    {
        var spec = new PlaylistDetailedSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}