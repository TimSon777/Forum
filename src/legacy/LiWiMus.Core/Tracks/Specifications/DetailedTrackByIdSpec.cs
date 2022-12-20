using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Tracks.Specifications;

public sealed class DetailedTrackByIdSpec : Specification<Track>, ISingleResultSpecification
{
    public DetailedTrackByIdSpec(int id)
    {
        Query.Where(track => track.Id == id)
             .Include(track => track.Genres)
             .Include(track => track.Owners)
             .Include(track => track.Album);
    }
}

public static partial class TracksRepositoryExtensions
{
    public static async Task<Track?> GetDetailedAsync(this IRepository<Track> repository, int id)
    {
        var spec = new DetailedTrackByIdSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}