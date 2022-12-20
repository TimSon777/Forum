#region

#endregion

namespace LiWiMus.Web.MVC.Areas.Artist.ViewModels;

public class CreateAlbumViewModel
{
    public string Title { get; set; } = null!;
    public IFormFile Cover { get; set; } = null!;
    public DateOnly PublishedAt { get; set; }
}