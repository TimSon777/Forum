using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Server.Features.Issuer;

[ApiController]
[Route("auth")]
public sealed class Controller : ControllerBase
{
    private readonly IJwtProvider _jwtProvider;

    public Controller(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }

    [HttpPost("connect/token")]
    public async Task<IActionResult> GetToken([FromForm] Request request)
    {
        var jwtRequest = request.Map();
        
        var jwtResponse = await _jwtProvider.IssueJwtAsync(jwtRequest);
        
        var response = jwtResponse.Map();
        
        return Ok(response);
    }
}