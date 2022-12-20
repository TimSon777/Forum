using FluentValidation;
using FluentValidation.Results;
using LiWiMus.Core.Constants;

namespace LiWiMus.Web.Shared.Extensions;

public static class FluentValidationExtensions
{
    public static IDictionary<string, string[]> ToDictionary(this ValidationResult validationResult)
    {
        return validationResult.Errors
                               .GroupBy(x => x.PropertyName)
                               .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());
    }

    public static IRuleBuilderOptions<T, string> DisableTags<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches(RegularExpressions.DisableTags);
    }
}