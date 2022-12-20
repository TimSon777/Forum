using System.Linq.Expressions;
using Ardalis.Specification;
using LiWiMus.Core.Shared;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Albums.Specifications;

public sealed class AlbumPaginatedSpec : Specification<Album>
{
    public AlbumPaginatedSpec(Pagination pagination)
    {
        Expression<Func<Album, object?>> sorting = p => 
            pagination.Sort.SortingBy == SortingBy.Popularity 
                ? p.Subscribers.Count 
                : p.Title;
        
        Query.Where(x => x.Title.Contains(pagination.Title))
            .Include(x => x.Owners)
            .Paginate(pagination.Page, pagination.ItemsPerPage, sorting, pagination.Sort.Order);
    }
}

public static partial class AlbumsRepositoryExtensions
{
    public static async Task<List<Album>> PaginateWithTitleAsync(this IRepository<Album> repository, Pagination pagination)
    {
        var spec = new AlbumPaginatedSpec(pagination);
        return await repository.ListAsync(spec);
    }
}