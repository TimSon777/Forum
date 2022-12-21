using System.Text;
using Microsoft.IdentityModel.Tokens;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public sealed class AuthSettings : ISettings
{
    public static string SectionName => "AUTH_SETTINGS";

    [ConfigurationKeyName("SECRET_KEY")]
    public string SecretKey { get; set; } = default!;

    public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
}