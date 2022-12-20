namespace LiWiMus.Web.MVC.Areas.Artist.ViewModels;

public class CreateTrackViewModel
{
    public int AlbumId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly PublishedAt { get; set; }
    public IFormFile File { get; set; } = null!;
}