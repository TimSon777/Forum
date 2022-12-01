using Metadata.API.Features.MetadataSaver;
using Metadata.API.Features.MetadataSaver.Requests;
using Metadata.Application;
using Metadata.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Metadata.API.Features;

[ApiController]
[Route("metadata")]
public sealed class MetadataController : ControllerBase
{
    private readonly IMetadataProvider _metadataProvider;

    public MetadataController(IMetadataProvider metadataProvider)
    {
        _metadataProvider = metadataProvider;
    }

    [HttpPost]
    public async Task<IActionResult> SaveMetadataAsync(SaveMetadataRequest request)
    {
        var metadata = request.Map();
        await _metadataProvider.SaveMetadataAsync(metadata);
        return Ok();
    }
}