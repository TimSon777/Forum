namespace Auth.Server.Features.Issuer;

public sealed class Request
{
    public string UserName { get; set; } = default!;
    public bool IsAdmin { get; set; }
}