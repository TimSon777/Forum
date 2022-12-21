namespace Infrastructure.Data;

public sealed class JwtRequest
{
    public string UserName { get; set; } = default!;
    public bool IsAdmin { get; set; }
}