using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Implementations;

public sealed class FileMover : IFileMover
{
    private readonly IAmazonS3 _amazonS3;
    private readonly FileServerSettings _fileServerSettings;

    public FileMover(IAmazonS3 amazonS3, IOptions<FileServerSettings> fileServerSettingsOptions)
    {
        _amazonS3 = amazonS3;
        _fileServerSettings = fileServerSettingsOptions.Value;
    }

    public async Task<bool> MoveToPersistenceAsync(Guid fileKey)
    {
        var getResponse = await _amazonS3.GetObjectAsync(_fileServerSettings.TemporaryBucketName, fileKey.ToString());

        if (getResponse is null)
        {
            return false;
        }

        if (getResponse.HttpStatusCode != HttpStatusCode.OK)
        {
            return false;
        }

        var putRequest = new PutObjectRequest
        {
            Key = fileKey.ToString(),
            BucketName = _fileServerSettings.PersistenceBucketName,
            InputStream = getResponse.ResponseStream,
            ContentType = getResponse.Headers.ContentType
        };

        var putResponse = await _amazonS3.PutObjectAsync(putRequest);
        
        return putResponse.HttpStatusCode == HttpStatusCode.OK;
    }
}