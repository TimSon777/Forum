using Ardalis.Specification;
using LiWiMus.Core.Playlists.Specifications;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Tracks.Specifications;

public sealed class TrackWithArtistsByIdSpecification : Specification<Track>, ISingleResultSpecification
{
    public TrackWithArtistsByIdSpecification(int trackId)
    {
        Query
            .Where(track => track.Id == trackId)
            .Include(track => track.Owners);
    }
}

public static partial class TracksRepositoryExtensions
{
    public static async Task<Track?> GetTrackWithArtistsAsync(this IRepository<Track> repository, int id)
    {
        var spec = new TrackWithArtistsByIdSpecification(id);
        return await repository.GetBySpecAsync(spec);
    }
}