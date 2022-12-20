using System.Linq.Expressions;
using Ardalis.Specification;
using LiWiMus.Core.Playlists.Specifications;
using LiWiMus.Core.Shared;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Tracks.Specifications;

public sealed class TracksPaginatedSpec : Specification<Track>
{
    public TracksPaginatedSpec(Pagination pagination)
    {
        Expression<Func<Track, object?>> sorting = p => 
            pagination.Sort.SortingBy == SortingBy.Popularity 
                ? p.Subscribers.Count 
                : p.Name;
        
        Query
            .Where(t => t.Name.Contains(pagination.Title))
            .Include(t => t.Album)
            .ThenInclude(a => a.Subscribers)
            .Include(t => t.Owners)
            .Paginate(pagination.Page, pagination.ItemsPerPage, sorting, pagination.Sort.Order);
    }
}

public static partial class TracksRepositoryExtensions
{
    public static async Task<List<Track>> PaginateWithTitleAsync(this IRepository<Track> repository, Pagination pagination)
    {
        var spec = new TracksPaginatedSpec(pagination);
        return await repository.ListAsync(spec);
    }
}