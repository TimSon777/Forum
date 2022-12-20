using System.Linq.Expressions;
using Ardalis.Specification;
using LiWiMus.Core.Genres;
using LiWiMus.Core.Shared;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Artists.Specifications;

public sealed class ArtistPaginatedSpec : Specification<Artist>
{
    public ArtistPaginatedSpec(Pagination pagination)
    {
        Expression<Func<Artist, object?>> sorting = p => 
            pagination.Sort.SortingBy == SortingBy.Popularity 
                ? p.Subscribers.Count 
                : p.Name;

        Query
            .Where(x => x.Name.Contains(pagination.Title))
            .Paginate(pagination.Page, pagination.ItemsPerPage, sorting, pagination.Sort.Order);
    }    
}

public static partial class ArtistsRepositoryExtensions
{
    public static async Task<List<Artist>> PaginateAsync(this IRepository<Artist> repository, Pagination pagination)
    {
        var spec = new ArtistPaginatedSpec(pagination);
        return await repository.ListAsync(spec);
    }
}