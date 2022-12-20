using Domain.Events;
using Infrastructure.Abstractions;
using MassTransit;
using MinimalApi.Endpoint;

namespace Metadata.API.Features.Metadata.Create;

public sealed class Endpoint : IEndpoint<IResult, Request>
{
    private readonly ICachingService _cachingService;
    private readonly IBus _bus;

    public Endpoint(ICachingService cachingService, IBus bus)
    {
        _cachingService = cachingService;
        _bus = bus;
    }

    public async Task<IResult> HandleAsync(Request request)
    {
        await _cachingService.SaveMetadataAsync(request.RequestId, request.Metadata);

        var fileUploadedEvent = new FileMetadataEvent
        {
            RequestId = request.RequestId
        };

        await _bus.Publish(fileUploadedEvent);
        return Results.Ok();
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("metadata", HandleAsync);
    }
}