using LiWiMus.Core.Interfaces.Files;
using LiWiMus.Core.Interfaces.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace LiWiMus.Web.Shared.Configuration;

public static class ConfigureMicroServices
{
    public static void AddMicroServices(this WebApplicationBuilder builder)
    {
        var section = builder.Configuration.GetRequiredSection(nameof(PullUrls));
        var pullUrls = section.Get<PullUrls>();
        builder.Services.Configure<PullUrls>(section);
        builder.Services
               .AddRefitClient<IMailService>()
               .ConfigureHttpClient(c =>
               {
                   c.BaseAddress = new Uri(pullUrls.MailServer);
               });
        builder.Services
               .AddRefitClient<IFileService>()
               .ConfigureHttpClient(c => { c.BaseAddress = new Uri(pullUrls.FileServer); });
    }
}