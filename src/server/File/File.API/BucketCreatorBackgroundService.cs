using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using File.API.Configurations;
using Microsoft.Extensions.Options;

namespace File.API;

public sealed class BucketCreatorBackgroundService : BackgroundService
{
    private readonly IAmazonS3 _amazonS3;
    private readonly FileServerSettings _fileServerSettings;

    public BucketCreatorBackgroundService(IAmazonS3 amazonS3, IOptions<FileServerSettings> fileServerSettingsOptions)
    {
        _amazonS3 = amazonS3;
        _fileServerSettings = fileServerSettingsOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var bucketExists = await _amazonS3.DoesS3BucketExistAsync(_fileServerSettings.BucketName);
        
        if (bucketExists)
        {
            return;
        }

        await CreateBucketAsync(stoppingToken);
    }

    private async Task CreateBucketAsync(CancellationToken token = new())
    {
        var request = new PutBucketRequest
        {
            BucketName = _fileServerSettings.BucketName,
            CannedACL = S3CannedACL.PublicReadWrite,
            UseClientRegion = true
        };

        var response = await _amazonS3.PutBucketAsync(request, token);

        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new AggregateException("Can't create bucket");
        }
    }
}