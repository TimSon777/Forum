using Application.Abstractions;
using Infrastructure.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace File.API.Features.File.Create;

[ApiController]
[Route("file")]
public sealed class Controller : ControllerBase
{
    private readonly IFileProvider _fileProvider;
    private readonly ICachingService _cachingService;
    private readonly IBus _bus;

    public Controller(IFileProvider fileProvider, ICachingService cachingService, IBus bus)
    {
        _fileProvider = fileProvider;
        _cachingService = cachingService;
        _bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] Request request)
    {
        var file = request.MapToSave();
        var result = await _fileProvider.SaveFileAsync(file);

        if (!result.Succeeded)
        {
            return Problem(result.Error);
        }

        var fileUploaded = request.MapToEvent();
        await _cachingService.SaveFileIdAsync(request.RequestId, result.Value.FileKey);
        await _bus.Publish(fileUploaded);
        return Ok(result.Value.FileKey);
    }
}