using AutoMapper;

namespace LiWiMus.Web.Shared.Extensions;

public static class MapperExtensions
{
    public static IEnumerable<TOut> MapList<TIn, TOut>(this IMapper mapper, IEnumerable<TIn> source) 
        => mapper.ProjectTo<TOut>(source.AsQueryable());
}