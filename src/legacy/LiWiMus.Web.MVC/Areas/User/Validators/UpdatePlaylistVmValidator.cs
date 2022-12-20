using FluentValidation;
using LiWiMus.Web.MVC.Areas.User.ViewModels;
using LiWiMus.Web.Shared.Extensions;

namespace LiWiMus.Web.MVC.Areas.User.Validators;

public class UpdatePlaylistVmValidator : AbstractValidator<UpdatePlaylistViewModel>
{
    public UpdatePlaylistVmValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty()
            .MaximumLength(50)
            .DisableTags();
    }
}