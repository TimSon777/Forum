using Infrastructure.Data;

namespace Auth.Server.Features.Issuer;

public static class Mapping
{
    public static Response Map(this JwtResponse jwtResponse) => new()
    {
        AccessToken = jwtResponse.Jwt
    };

    public static JwtRequest Map(this Request request) => new()
    {
        IsAdmin = request.IsAdmin,
        UserName = request.UserName
    };
}