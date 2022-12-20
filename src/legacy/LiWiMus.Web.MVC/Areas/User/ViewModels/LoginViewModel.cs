using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class LoginViewModel
{
    [Required] public string UserName { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string Password { get; set; } = "";
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public bool RememberMe { get; set; }
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string? ReturnUrl { get; set; }

    public IList<AuthenticationScheme>? ExternalLogins { get; set; }
}