using Infrastructure.Data;

namespace Infrastructure.Abstractions;

public interface IJwtProvider
{
    Task<JwtResponse> IssueJwtAsync(JwtRequest jwtRequest);
}