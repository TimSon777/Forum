#region

using System.ComponentModel.DataAnnotations;
using LiWiMus.SharedKernel;

#endregion

namespace LiWiMus.Web.MVC.Areas.User.ViewModels;

public class ProfileViewModel : HasId
{
    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? Patronymic { get; set; }

    public bool IsMale { get; set; }

    [DataType(DataType.Date)] public DateOnly? BirthDate { get; set; }
    public bool IsAccountOwner { get; set; }
    public bool IsSubscribed { get; set; }
    public IFormFile? Avatar { get; set; }
    public bool EmailConfirmed { get; set; }
}