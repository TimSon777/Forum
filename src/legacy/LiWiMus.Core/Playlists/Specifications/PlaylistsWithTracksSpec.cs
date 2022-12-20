using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Playlists.Specifications;

public sealed class PlaylistsWithTracksSpec : Specification<Playlist>, ISingleResultSpecification
{
    public PlaylistsWithTracksSpec(int id)
    {
        Query
            .Where(album => album.Id == id)
            .Include(album => album.Tracks);
    }
}

public static partial class PlaylistsRepositoryExtensions
{
    public static async Task<Playlist?> GetPlaylistsWithTracksAsync(this IRepository<Playlist> repository, int id)
    {
        var spec = new PlaylistsWithTracksSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
} 