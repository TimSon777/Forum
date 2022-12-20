using LiWiMus.SharedKernel;

namespace LiWiMus.Web.MVC.ViewModels.ForListViewModels;

public class ArtistForListViewModel : HasId
{
    public string Name { get; set; } = null!;
    public string About { get; set; } = null!;
    public string PhotoLocation { get; set; } = null!;
}