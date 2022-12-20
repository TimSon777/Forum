using FluentValidation;
using LiWiMus.Web.MVC.Areas.User.ViewModels;

namespace LiWiMus.Web.MVC.Areas.User.Validators;

public class PaymentValidator : AbstractValidator<PaymentViewModel>
{
    public PaymentValidator()
    {
        RuleFor(model => model.Amount)
            .GreaterThan(0);

        RuleFor(model => model.CardNumber)
            .NotEmpty()
            .Matches("^[0-9]{16}$");

        RuleFor(model => model.CardExpires)
            .NotEmpty()
            .Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$");

        RuleFor(model => model.CvvCode)
            .NotEmpty()
            .Matches(@"^[0-9]{3,4}$");

        RuleFor(model => model.CardHolder)
            .NotEmpty();
    }
}