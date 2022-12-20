#region

#endregion

namespace LiWiMus.Web.MVC.Areas.Artist.ViewModels;

public class UpdateArtistViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string About { get; set; } = null!;

    public IFormFile? Photo { get; set; }
}