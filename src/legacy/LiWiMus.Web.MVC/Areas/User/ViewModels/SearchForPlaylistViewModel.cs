namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class SearchForPlaylistViewModel
{
    private string _title = "";

    public string Title
    {
        // ReSharper disable once ConstantNullCoalescingCondition
        get => _title ?? "";
        set => _title = value;
    }
    public int PlaylistId { get; set; }
}