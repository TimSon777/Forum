using System.Linq.Expressions;
using Ardalis.Specification;
using LiWiMus.Core.Shared;
using LiWiMus.Core.Tracks;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Users.Specifications;

public sealed class UsersPaginatedSpec : Specification<User>
{
    public UsersPaginatedSpec(Pagination pagination)
    {
        Expression<Func<User, object?>> sorting = p => 
            pagination.Sort.SortingBy == SortingBy.Popularity 
                ? p.Followers.Count 
                : p.UserName;
        
        Query
            .Where(t => t.UserName.Contains(pagination.Title));
        
        if (pagination.Sort.Order == Order.Asc)
        {
            Query.OrderBy(sorting);
        }
        else
        {
            Query.OrderByDescending(sorting);
        }
        
        Query
            .Skip((pagination.Page - 1) * pagination.ItemsPerPage)
            .Take(pagination.ItemsPerPage);
    }
}

public static partial class UsersRepositoryExtensions
{
    public static async Task<List<User>> PaginateWithTitleAsync(this IRepository<User> repository, Pagination pagination)
    {
        var spec = new UsersPaginatedSpec(pagination);
        return await repository.ListAsync(spec);
    }
}