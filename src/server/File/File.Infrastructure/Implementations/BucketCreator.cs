using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using File.Application.Abstractions;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace File.Infrastructure.Implementations;

public sealed class BucketCreator : IBucketCreator
{
    private readonly FileServerSettings _fileServerSettings;
    private readonly IAmazonS3 _amazonS3;

    public BucketCreator(IOptions<FileServerSettings> fileServerSettingsOptions, IAmazonS3 amazonS3)
    {
        _amazonS3 = amazonS3;
        _fileServerSettings = fileServerSettingsOptions.Value;
    }

    public async Task CreatePersistenceBucketIfNotExistAsync()
    {
        await CreateBucketIfNotExistAsync(_fileServerSettings.PersistenceBucketName);
    }

    public async Task CreateTemporaryBucketIfNotExistAsync()
    {
        await CreateBucketIfNotExistAsync(_fileServerSettings.TemporaryBucketName);
    }

    private async Task CreateBucketIfNotExistAsync(string bucketName)
    {
        var bucketExists = await _amazonS3.DoesS3BucketExistAsync(bucketName);
        
        if (bucketExists)
        {
            return;
        }
        
        var request = new PutBucketRequest
        {
            BucketName = bucketName
        };

        var response = await _amazonS3.PutBucketAsync(request);

        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new AggregateException("Can't create bucket");
        }
    }
}