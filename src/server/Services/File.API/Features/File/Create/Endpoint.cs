using Application.Abstractions;
using Infrastructure.Abstractions;
using MassTransit;
using MinimalApi.Endpoint;

namespace File.API.Features.File.Create;

public sealed class Endpoint : IEndpoint<IResult, Request>
{
    private readonly IFileProvider _fileProvider;
    private readonly ICachingService _cachingService;
    private readonly IBus _bus;

    public Endpoint(IFileProvider fileProvider, IBus bus, ICachingService cachingService)
    {
        _fileProvider = fileProvider;
        _bus = bus;
        _cachingService = cachingService;
    }

    public async Task<IResult> HandleAsync(Request request)
    {
        var file = request.MapToSave();
        var result = await _fileProvider.SaveFileAsync(file);

        if (!result.Succeeded)
        {
            return Results.Problem(result.Error);
        }

        var fileUploaded = request.MapToEvent();
        await _cachingService.SaveFileIdAsync(request.RequestId, result.Value.FileKey);
        await _bus.Publish(fileUploaded);
        return Results.Ok(result.Value.FileKey);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("file", HandleAsync);
    }
}