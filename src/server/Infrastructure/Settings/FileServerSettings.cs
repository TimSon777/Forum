// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public sealed class FileServerSettings : ISettings
{
    public static string SectionName => "FILE_SERVER_SETTINGS";

    [ConfigurationKeyName("PASSWORD")]
    public string Password { get; set; } = default!;

    [ConfigurationKeyName("USER")]
    public string User { get; set; } = default!;

    [ConfigurationKeyName("HOST")]
    public string Host { get; set; } = default!;

    [ConfigurationKeyName("PERSISTENCE_BUCKET_NAME")] 
    public string PersistenceBucketName { get; set; } = default!;
    
    [ConfigurationKeyName("TEMPORARY_BUCKET_NAME")] 
    public string TemporaryBucketName { get; set; } = default!;

}