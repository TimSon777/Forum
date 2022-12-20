using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace LiWiMus.Web.Shared.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static RouteHandlerBuilder MapPatch(this IEndpointRouteBuilder builder, string pattern, Delegate handler)
    {
        return builder.MapMethods(pattern, new [] {"PATCH"}, handler);
    }
}