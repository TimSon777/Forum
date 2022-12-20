namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class PaymentViewModel
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public int Amount { get; set; }
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string ReturnUrl { get; set; } = null!;
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string? Reason { get; set; }
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string CardNumber { get; set; } = "";
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string CardExpires { get; set; } = "";
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string CardHolder { get; set; } = "";
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string CvvCode { get; set; } = "";
}