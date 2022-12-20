using LiWiMus.Web.Shared.Services;
using LiWiMus.Web.Shared.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LiWiMus.Web.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        services.AddTransient<IFormFileSaver, FormFileSaver>();
        return services;
    }
}