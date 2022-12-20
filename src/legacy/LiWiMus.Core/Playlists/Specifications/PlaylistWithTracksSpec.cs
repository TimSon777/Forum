using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Playlists.Specifications;

public sealed class PlaylistWithTracksSpec : Specification<Playlist>, ISingleResultSpecification
{
    public PlaylistWithTracksSpec(int id)
    {
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Tracks);
    }
}

public static partial class PlaylistsRepositoryExtensions
{
    public static async Task<Playlist?> WithTracksAsync(this IRepository<Playlist> repository, int id)
    {
        var spec = new PlaylistWithTracksSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}