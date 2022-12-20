using FluentValidation;
using LiWiMus.Web.MVC.Areas.User.ViewModels;

namespace LiWiMus.Web.MVC.Areas.User.Validators;

public class UpdatePlaylistPhotoVmValidator : AbstractValidator<UpdatePlaylistPhotoViewModel>
{
    public UpdatePlaylistPhotoVmValidator()
    {
        RuleFor(model => model.Photo)
            .NotEmpty()
            .Must(photo => photo.ContentType.StartsWith("image"))
            .WithMessage("Bad image");
    }
}