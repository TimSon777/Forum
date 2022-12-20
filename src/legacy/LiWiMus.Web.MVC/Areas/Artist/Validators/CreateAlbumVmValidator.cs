using FluentValidation;
using LiWiMus.Web.MVC.Areas.Artist.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Artist.Validators;

public class CreateAlbumVmValidator : AbstractValidator<CreateAlbumViewModel>
{
    public CreateAlbumVmValidator()
    {
        RuleFor(model => model.Cover)
            .NotEmpty();
        RuleFor(model => model.Title)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(model => model.PublishedAt)
            .NotEmpty()
            .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("The publication date must be less than or equal to the current date in Utc format");
        
        RuleFor(model => model.Cover)
            .NotEmpty()
            .Must(cover => cover.ContentType.StartsWith("image"))
            .WithMessage("Bad image");
    }
}