using System.ComponentModel.DataAnnotations;

namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class ResetPasswordViewModel
{
    [Required] 
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string UserId { get; set; } = "";

    [Required] 
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string Token { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string Password { get; set; } = "";
}