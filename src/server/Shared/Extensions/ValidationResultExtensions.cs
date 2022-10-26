using FluentValidation.Results;

// ReSharper disable once CheckNamespace
namespace FluentValidation;

public static class ValidationResultExtensions
{
    public static void EnsureSuccess(this ValidationResult validationResult)
    {
        Guard.Against.Null(validationResult);
        
        if (validationResult.IsValid)
        {
            return;
        }

        throw new ValidationException("Validation exception", validationResult.Errors);
    }
}