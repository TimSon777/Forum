using System.Linq.Expressions;
using Ardalis.Specification;
using LiWiMus.Core.Shared;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Playlists.Specifications;

public sealed class PlaylistsPaginatedSpec : Specification<Playlist>
{
    public PlaylistsPaginatedSpec(Pagination pagination)
    {
        Expression<Func<Playlist, object?>> sorting = p => 
            pagination.Sort.SortingBy == SortingBy.Popularity 
                ? p.Subscribers.Count 
                : p.Name;
        
        Query
            .Where(p => p.IsPublic && p.Name.Contains(pagination.Title))
            .Include(p => p.Owner)
            .Paginate(pagination.Page, pagination.ItemsPerPage, sorting, pagination.Sort.Order);
    }
}

public static partial class PlaylistsRepositoryExtensions
{
    public static async Task<List<Playlist>> PaginateWithTitleAsync(this IRepository<Playlist> repository, Pagination pagination)
    {
        var spec = new PlaylistsPaginatedSpec(pagination);
        return await repository.ListAsync(spec);
    }
}