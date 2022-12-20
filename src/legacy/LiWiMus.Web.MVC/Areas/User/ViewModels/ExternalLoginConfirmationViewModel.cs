using System.ComponentModel.DataAnnotations;

namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class ExternalLoginConfirmationViewModel
{
    public string? ReturnUrl { get; set; }
    public string? ProviderDisplayName { get; set; }

    [Required] public string UserName { get; set; } = null!;
}