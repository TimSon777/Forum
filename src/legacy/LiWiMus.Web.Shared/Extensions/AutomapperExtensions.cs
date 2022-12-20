using AutoMapper;

namespace LiWiMus.Web.Shared.Extensions;

public static class AutomapperExtensions
{
    public static void IgnoreNulls<TSource, TDestination>(this IMappingExpression<TSource, TDestination> exp)
    {
        exp.ForAllMembers(opt => opt.Condition((_, _, srcMember) =>
        {
            switch (srcMember)
            {
                case null:
                case DateOnly dateOnly when dateOnly == default:
                    return false;
                default:
                    return true;
            }
        }));
    }
}