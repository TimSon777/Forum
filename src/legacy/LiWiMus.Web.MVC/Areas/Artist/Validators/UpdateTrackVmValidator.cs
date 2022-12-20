#region

using FluentValidation;
using LiWiMus.Web.MVC.Areas.Artist.ViewModels;
using LiWiMus.Web.Shared.Extensions;

#endregion

namespace LiWiMus.Web.MVC.Areas.Artist.Validators;

public class UpdateTrackVmValidator : AbstractValidator<UpdateTrackViewModel>
{
    public UpdateTrackVmValidator()
    {
        RuleFor(model => model.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50)
            .DisableTags();

        RuleFor(model => model.Id)
            .NotEmpty();

        RuleFor(r => r.PublishedAt)
            .NotEmpty()
            .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("The publication date must be less than or equal to the current date in Utc format");

        RuleFor(model => model.ArtistsIds)
            .NotEmpty()
            .When(model => model.ArtistsIds is not null);

        RuleFor(model => model.GenresIds)
            .NotEmpty()
            .When(model => model.GenresIds is not null);

        RuleFor(model => model.File)
            .Must(file => file!.ContentType.StartsWith("audio"))
            .When(model => model.File is not null)
            .WithMessage("Bad audio file");
    }
}