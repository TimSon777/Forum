using System.ComponentModel.DataAnnotations;

namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class RegisterViewModel
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    [Required] public string UserName { get; set; } = "";
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    [Required] [EmailAddress] public string Email { get; set; } = "";
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string PasswordConfirm { get; set; } = "";
}