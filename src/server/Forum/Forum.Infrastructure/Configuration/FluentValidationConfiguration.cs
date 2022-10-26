using System.Reflection;
using FluentValidation;
using Forum.Infrastructure.MessageHandlers.Validators;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class FluentValidationConfiguration
{
    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(GetMessageHubItemValidator)) 
                       ?? throw new AggregateException();
        
        var assemblies = new List<Assembly> { assembly };
        services.AddValidatorsFromAssemblies(assemblies);
        return services;
    }
}