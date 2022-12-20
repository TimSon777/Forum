#region

using FluentValidation;
using LiWiMus.Web.MVC.Areas.Artist.ViewModels;
using LiWiMus.Web.Shared.Extensions;

#endregion

namespace LiWiMus.Web.MVC.Areas.Artist.Validators;

public class CreateTrackVmValidator : AbstractValidator<CreateTrackViewModel>
{
    public CreateTrackVmValidator()
    {
        RuleFor(model => model.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .DisableTags();
        
        RuleFor(r => r.PublishedAt)
            .NotEmpty()
            .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("The publication date must be less than or equal to the current date in Utc format");

        RuleFor(model => model.AlbumId)
            .NotEmpty();

        RuleFor(model => model.File)
            .NotEmpty()
            .Must(file => file.ContentType.StartsWith("audio"))
            .WithMessage("Bad audio file");
    }
}