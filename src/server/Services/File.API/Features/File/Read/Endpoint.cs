using Application.Abstractions;
using MinimalApi.Endpoint;

namespace File.API.Features.File.Read;

public sealed class Endpoint : IEndpoint<IResult, string>
{
    private readonly IFileProvider _fileProvider;

    public Endpoint(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    public async Task<IResult> HandleAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return Results.Problem();
        }

        var result = await _fileProvider.FindFileAsync(key);

        return !result.Succeeded
            ? Results.UnprocessableEntity()
            : Results.File(result.Value.File, result.Value.ContentType, key);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("file/{key}", HandleAsync);
    }
}