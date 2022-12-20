namespace LiWiMus.Web.MVC.Areas.Artist.ViewModels;

public class UpdateTrackViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly PublishedAt { get; set; }
    public IFormFile? File { get; set; }
    public int[]? ArtistsIds { get; set; }
    public int[]? GenresIds { get; set; }
}