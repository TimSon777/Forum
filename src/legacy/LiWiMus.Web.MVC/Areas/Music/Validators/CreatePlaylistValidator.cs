using FluentValidation;
using LiWiMus.Web.MVC.Areas.Music.ViewModels;
using LiWiMus.Web.MVC.Areas.User.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Music.Validators;

// ReSharper disable once UnusedType.Global
public class CreatePlaylistValidator : AbstractValidator<CreatePlaylistViewModel>
{
    public CreatePlaylistValidator()
    {
        RuleFor(x => x.Name)
            .Length(5, 50)
            .NotEmpty();
    }
}