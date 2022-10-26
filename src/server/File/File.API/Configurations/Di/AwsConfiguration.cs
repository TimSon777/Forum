using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using File.API;
using File.API.Configurations;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class AwsConfiguration
{
    public static IServiceCollection AddAws(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.Get<FileServerSettings>(FileServerSettings.SectionName);
        services.Configure<FileServerSettings>(configuration, FileServerSettings.SectionName);
        
        var options = new AWSOptions
        {
            Credentials = new BasicAWSCredentials(settings.User, settings.Password),
            DefaultClientConfig =
            {
                ServiceURL = settings.Host
            }
        };
            
        services.AddDefaultAWSOptions(options);
        services.AddAWSService<IAmazonS3>();
        services.AddHostedService<BucketCreatorBackgroundService>();
        return services;
    }
}