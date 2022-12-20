using FluentValidation;
using LiWiMus.Web.MVC.Areas.Search.ViewModels;

namespace LiWiMus.Web.MVC.Areas.Search.Validator;

// ReSharper disable once UnusedType.Global
public class SearchValidator : AbstractValidator<SearchViewModel>
{
    public SearchValidator()
    {
        RuleFor(x => x.Title)
            .Length(0, 50);

        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.ItemsPerPage)
            .GreaterThan(0);
    }
}