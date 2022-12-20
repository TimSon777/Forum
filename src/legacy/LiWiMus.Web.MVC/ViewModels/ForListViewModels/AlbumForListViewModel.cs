using LiWiMus.SharedKernel;
using LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;

namespace LiWiMus.Web.MVC.ViewModels.ForListViewModels;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class AlbumForListViewModel : HasId
{
    public string Title { get; set; } = null!;
    public DateOnly PublishedAt { get; set; }
    public string CoverLocation { get; set; } = null!;
    public virtual ICollection<ArtistGeneralInfoViewModel> Owners { get; set; } = null!;
}