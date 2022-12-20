using FluentValidation;
using LiWiMus.Core.Constants;
using LiWiMus.Web.Areas.Admin.ViewModels;
using LiWiMus.Web.Extensions;

namespace LiWiMus.Web.Areas.Admin.Validators;

public class RoleValidator : AbstractValidator<RoleViewModel>
{
    public RoleValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(20)
            .DisableTags();
        RuleFor(model => model.Description)
            .NotEmpty()
            .MaximumLength(100)
            .DisableTags();
        RuleFor(model => model.PricePerMonth)
            .GreaterThanOrEqualTo(0);
    }
}