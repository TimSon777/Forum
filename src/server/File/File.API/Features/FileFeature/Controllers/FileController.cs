using File.API.Features.FileFeature.Requests;
using File.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace File.API.Features.FileFeature.Controllers;

[ApiController]
[Route("file")]
public class FileController : ControllerBase
{
    private readonly IFileProvider _fileProvider;

    public FileController(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> GetAsync(string key, CancellationToken token = new())
    {
        var result = await _fileProvider.FindFileAsync(key, token);

        if (!result.Succeeded)
        {
            return UnprocessableEntity($"File not found by key {key}. Error: {result.Error}");
        }

        return File(
            result.Value!.Body, 
            result.Value.ContentType, 
            result.Value.Name);
    }

    [HttpPost]
    public async Task<IActionResult> SaveAsync([FromForm] SaveFileRequest request, CancellationToken token = new())
    {
        await _fileProvider.SaveFileAsync(request.RequestId, request.File, token);
        return Ok();
    }
}