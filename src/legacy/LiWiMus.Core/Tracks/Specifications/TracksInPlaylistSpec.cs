using Ardalis.Specification;
using LiWiMus.Core.Playlists;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Tracks.Specifications;

public sealed class TracksInPlaylistSpec : Specification<Track>
{
    public TracksInPlaylistSpec(int playlistId, int page, int tracksPerPage)
    {
        Query.Where(track => track.Playlists.Any(playlist => playlist.Id == playlistId))
             .Paginate(page, tracksPerPage)
             .Include(track => track.Album);
    }

    public TracksInPlaylistSpec(int playlistId)
    {
        Query.Where(track => track.Playlists.Any(playlist => playlist.Id == playlistId))
             .Include(track => track.Album);
    }
}

public static partial class TracksRepositoryExtensions
{
    public static async Task<List<Track>> ListInPlaylistAsync(this IRepository<Track> repository, Playlist playlist,
                                                              int page,
                                                              int tracksPerPage)
    {
        var tracksInPlaylistSpec = new TracksInPlaylistSpec(playlist.Id, page, tracksPerPage);
        return await repository.ListAsync(tracksInPlaylistSpec);
    }

    public static async Task<List<Track>> ListInPlaylistAsync(this IRepository<Track> repository, Playlist playlist)
    {
        var tracksInPlaylistSpec = new TracksInPlaylistSpec(playlist.Id);
        return await repository.ListAsync(tracksInPlaylistSpec);
    }

    public static async Task<int> CountInPlaylistAsync(this IRepository<Track> repository, Playlist playlist)
    {
        var tracksInPlaylistSpec = new TracksInPlaylistSpec(playlist.Id);
        return await repository.CountAsync(tracksInPlaylistSpec);
    }
}