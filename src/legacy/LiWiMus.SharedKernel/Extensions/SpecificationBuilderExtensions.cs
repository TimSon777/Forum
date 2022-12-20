using System.Linq.Expressions;
using Ardalis.Specification;

namespace LiWiMus.SharedKernel.Extensions;

public static class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<T> Paginate<T>(this ISpecificationBuilder<T> specificationBuilder, int page,
                                                       int itemsPerPage,  Expression<Func<T,object?>>? orderBy = null, Order order = Order.Asc) where T : BaseEntity
    {
        orderBy ??= x => x.Id;
        if (order == Order.Asc)
        {
            specificationBuilder.OrderBy(orderBy);
        }
        else
        {
            specificationBuilder.OrderByDescending(orderBy);
        }
        
        return specificationBuilder
               .Skip((page - 1) * itemsPerPage)
               .Take(itemsPerPage);
    }
}