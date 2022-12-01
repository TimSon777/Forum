using FluentValidation;
using Forum.Infrastructure.MessageHandlers.Validators;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class FluentValidationConfiguration
{
    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<GetMessageHubItemValidator>();
        return services;
    }
}