using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.LikedPlaylists.Specifications;

public sealed class LikedPlaylistsByPlaylistIdSpec : Specification<LikedPlaylist>
{
    public LikedPlaylistsByPlaylistIdSpec(int playlistId)
    {
        Query.Where(likedPlaylist => likedPlaylist.Playlist.Id == playlistId);
    }
}

public static partial class LikedPlaylistsRepositoryExtensions
{
    public static async Task<int> CountSubscribersAsync(this IRepository<LikedPlaylist> repository, int playlistId)
    {
        var spec = new LikedPlaylistsByPlaylistIdSpec(playlistId);
        return await repository.CountAsync(spec);
    }
}