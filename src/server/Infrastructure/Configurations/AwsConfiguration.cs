using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class AwsConfiguration
{
    public static IServiceCollection AddAws(this IServiceCollection services, IConfiguration configuration)
    {
        AWSConfigs.LoggingConfig.LogResponses = ResponseLoggingOption.Always;
        AWSConfigs.LoggingConfig.LogTo = LoggingOptions.SystemDiagnostics;
        AWSConfigs.AddTraceListener("Amazon", new System.Diagnostics.ConsoleTraceListener());
        
        var settings = configuration.GetSettings<FileServerSettings>();
        services.ConfigureSettings<FileServerSettings>(configuration);

        var credentials = new BasicAWSCredentials(settings.User, settings.Password);
        var config = new AmazonS3Config
        {
            ForcePathStyle = true,
            ServiceURL = settings.Host
        };

        var client = new AmazonS3Client(credentials, config);
        services.AddSingleton<IAmazonS3>(client);

        return services;
    }
}