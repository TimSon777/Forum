using LiWiMus.SharedKernel;

namespace LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;

public class PlaylistGeneralInfoViewModel : HasId
{
    public string Name { get; set; } = "";
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public string PhotoLocation { get; set; } = "";
}