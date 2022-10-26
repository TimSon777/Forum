namespace File.API.Configurations;

public sealed class FileServerSettings
{
    public const string SectionName = "FILE_SERVER_SETTINGS";

    [ConfigurationKeyName("PASSWORD")]
    public string Password { get; set; } = default!;

    [ConfigurationKeyName("USER")]
    public string User { get; set; } = default!;

    [ConfigurationKeyName("HOST")]
    public string Host { get; set; } = default!;

    [ConfigurationKeyName("BUCKET_NAME")] 
    public string BucketName { get; set; } = default!;
}