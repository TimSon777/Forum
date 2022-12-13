using Infrastructure.Abstractions;
using MinimalApi.Endpoint;

namespace Metadata.API.Features.Metadata.Create;

public sealed class Endpoint : IEndpoint<IResult, Request>
{
    private readonly ICachingService _cachingService;

    public Endpoint(ICachingService cachingService)
    {
        _cachingService = cachingService;
    }

    public async Task<IResult> HandleAsync(Request request)
    {
        await _cachingService.SaveMetadataAsync(request.RequestId, request.Metadata);
        return Results.Ok();
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("metadata", HandleAsync);
    }
}