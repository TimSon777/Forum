using LiWiMus.SharedKernel;

namespace LiWiMus.Web.MVC.ViewModels.GeneralInfoViewModels;

public class UserGeneralInfoViewModel : HasId
{
    public string UserName { get; set; }  = "";
}