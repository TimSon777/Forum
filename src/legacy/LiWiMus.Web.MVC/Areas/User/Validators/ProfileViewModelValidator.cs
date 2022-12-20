#region

using FluentValidation;
using LiWiMus.Web.MVC.Areas.User.ViewModels;
using LiWiMus.Web.Shared.Extensions;

#endregion

namespace LiWiMus.Web.MVC.Areas.User.Validators;

// ReSharper disable once UnusedType.Global
public class ProfileViewModelValidator : AbstractValidator<ProfileViewModel>
{
    public ProfileViewModelValidator()
    {
        RuleFor(vm => vm.UserName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(20)
            .DisableTags();

        RuleFor(vm => vm.Email)
            .NotEmpty()
            .EmailAddress()
            .DisableTags();

        RuleFor(vm => vm.FirstName)
            .MinimumLength(3)
            .MaximumLength(20)
            .DisableTags();

        RuleFor(vm => vm.SecondName)
            .MinimumLength(3)
            .MaximumLength(30)
            .DisableTags();

        RuleFor(vm => vm.Patronymic)
            .MinimumLength(3)
            .MaximumLength(50)
            .DisableTags();

        RuleFor(vm => vm.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .GreaterThan(new DateOnly(1900, 1, 1));

        RuleFor(vm => vm.Avatar)
            .Must(avatar => avatar!.ContentType.StartsWith("image"))
            .When(vm => vm.Avatar is not null)
            .WithMessage("Bad image");
    }
}