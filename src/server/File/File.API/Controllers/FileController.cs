﻿using File.API.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace File.API.Controllers;

[ApiController]
[Route("file/[controller]")]
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
            return UnprocessableEntity($"File not found by key {key}");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> SaveAsync(IFormFile file, CancellationToken token = new())
    {
        var result = await _fileProvider.SaveFileAsync(file.OpenReadStream(), token);

        if (result.Succeeded)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }
}