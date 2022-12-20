using System.Linq.Expressions;
using Ardalis.Specification;
using LiWiMus.Core.Shared;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Genres.Specifications;

public sealed class GenrePaginatedSpec : Specification<Genre>
{
    public GenrePaginatedSpec(Pagination pagination)
    {
        Expression<Func<Genre, object?>> sorting = p => 
            pagination.Sort.SortingBy == SortingBy.Popularity 
                ? p.Tracks.Count 
                : p.Name;

        Query
            .Where(x => x.Name.Contains(pagination.Title))
            .Paginate(pagination.Page, pagination.ItemsPerPage, sorting, pagination.Sort.Order);
    }
}

public static partial class GenresRepositoryExtensions
{
    public static async Task<List<Genre>> PaginateAsync(this IRepository<Genre> repository, Pagination pagination)
    {
        var spec = new GenrePaginatedSpec(pagination);
        return await repository.ListAsync(spec);
    }
}