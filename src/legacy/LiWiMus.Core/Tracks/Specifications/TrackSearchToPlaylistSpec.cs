using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Tracks.Specifications;

public sealed class TrackSearchToPlaylistSpec : Specification<Track>
{
    public TrackSearchToPlaylistSpec(string title, int playlistId)
    {
        Query
            .Where(t => t.Name.Contains(title)
                        && t.Playlists.All(p => p.Id != playlistId));
    }
}

public static partial class TracksRepositoryExtensions
{
    public static async Task<List<Track>> SearchToPlaylistAsync(this IRepository<Track> repository, string title, int playlistId)
    {
        var spec = new TrackSearchToPlaylistSpec(title, playlistId);
        return await repository.ListAsync(spec);
    }
}