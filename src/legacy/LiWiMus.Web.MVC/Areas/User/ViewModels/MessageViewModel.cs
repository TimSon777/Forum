namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class MessageViewModel
{
    public UserChatViewModel Owner { get; set; } = null!;
    public string Text { get; set; } = "";
}