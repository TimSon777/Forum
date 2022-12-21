using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Infrastructure.Abstractions;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Implementations;

public sealed class JwtProvider : IJwtProvider
{
    private readonly AuthSettings _authSettings;
    
    public JwtProvider(IOptions<AuthSettings> authSettingsOptions)
    {
        _authSettings = authSettingsOptions.Value;
    }
    
    public Task<JwtResponse> IssueJwtAsync(JwtRequest jwtRequest)
    {
        var utcNow = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: null,
            audience: null,
            notBefore: utcNow,
            claims: new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, jwtRequest.UserName),
                new("admin", jwtRequest.IsAdmin.ToString())
            },
            expires: utcNow.AddYears(1),
            signingCredentials: new SigningCredentials(_authSettings.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var jwtResponse = new JwtResponse
        {
            Jwt = encodedJwt
        };
 
        return Task.FromResult(jwtResponse);
    }
}