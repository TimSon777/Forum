using LiWiMus.SharedKernel;

namespace LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;

public class AlbumGeneralInfoViewModel : HasId
{
    public string Title { get; set; } = "";
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    public string CoverLocation { get; set; } = "";
}