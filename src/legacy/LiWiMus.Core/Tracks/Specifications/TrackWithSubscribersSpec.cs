using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Tracks.Specifications;

public sealed class TrackWithSubscribersSpec : Specification<Track>, ISingleResultSpecification
{
    public TrackWithSubscribersSpec(int id)
    {
        Query
            .Where(track => track.Id == id)
            .Include(track => track.Subscribers)
            .ThenInclude(song => song.User);
    }
}

public static partial class TracksRepositoryExtensions
{
    public static async Task<Track?> WithSubscribersAsync(this IRepository<Track> repository, int id)
    {
        var spec = new TrackWithSubscribersSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}