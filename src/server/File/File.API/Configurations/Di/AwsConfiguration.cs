using Amazon;
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
        EnableLogging();
        var settings = configuration.Get<FileServerSettings>(FileServerSettings.SectionName);
        services.Configure<FileServerSettings>(configuration, FileServerSettings.SectionName);
        Environment.SetEnvironmentVariable("AWS_ENABLE_ENDPOINT_DISCOVERY", "false");
        Environment.SetEnvironmentVariable("endpoint_discovery_enabled", "false");
        var options = new AWSOptions
        {
            Credentials = new BasicAWSCredentials(settings.User, settings.Password),
            DefaultClientConfig =
            {
                ServiceURL = settings.Host,
            },
            Region = RegionEndpoint.EUWest1
        };
        
        services.AddDefaultAWSOptions(options);
        var credentials = new BasicAWSCredentials(settings.User, settings.Password);
        var config = new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.USEast1,
            ForcePathStyle = true,
            ServiceURL = settings.Host
        };
        var client = new AmazonS3Client(credentials, config);
        services.AddSingleton<IAmazonS3>(client);
        services.AddHostedService<BucketCreatorBackgroundService>();
        return services;
    }

    private static void EnableLogging()
    {
        AWSConfigs.LoggingConfig.LogResponses = ResponseLoggingOption.Always;
        AWSConfigs.LoggingConfig.LogTo = LoggingOptions.SystemDiagnostics;
        AWSConfigs.AddTraceListener("Amazon", new System.Diagnostics.ConsoleTraceListener());
    }
}