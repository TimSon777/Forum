using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Playlists.Specifications;

public sealed class PlaylistsByUserIdSpec : Specification<Playlist>
{
    public PlaylistsByUserIdSpec(int userId)
    {
        Query.Where(p => p.Owner.Id == userId);
    }
}

public static partial class PlaylistsRepositoryExtensions
{
    public static async Task<List<Playlist>> ByUserIdAsync(this IRepository<Playlist> repository, int userId)
    {
        var spec = new PlaylistsByUserIdSpec(userId);
        return await repository.ListAsync(spec);
    }
}