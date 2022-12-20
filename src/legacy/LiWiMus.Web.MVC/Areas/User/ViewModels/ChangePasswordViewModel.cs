using System.ComponentModel.DataAnnotations;

namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class ChangePasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string NewPassword { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string OldPassword { get; set; } = null!;
}