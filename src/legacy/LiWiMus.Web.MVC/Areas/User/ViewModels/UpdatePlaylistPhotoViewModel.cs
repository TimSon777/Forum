namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class UpdatePlaylistPhotoViewModel
{
    public int Id { get; set; }
    public IFormFile Photo { get; set; } = null!;
}