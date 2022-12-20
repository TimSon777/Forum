using LiWiMus.SharedKernel;
using LiWiMus.Web.MVC.ViewModels.ForListViewModels;

namespace LiWiMus.Web.MVC.Areas.Music.ViewModels;

public class GenreViewModel : HasId
{
    public string Name { get; set; }  = "";
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<TrackForListViewModel> Tracks { get; set; } = new();
}