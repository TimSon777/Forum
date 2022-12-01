using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using FileMetadata.Queue.Listener.Abstractions;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace FileMetadata.Queue.Listener.Implementations;

public sealed class FileMover : IFileMover
{
    private readonly IAmazonS3 _amazonS3;
    private readonly FileServerSettings _fileServerSettings;

    public FileMover(IAmazonS3 amazonS3, IOptions<FileServerSettings> fileServerSettingsOptions)
    {
        _amazonS3 = amazonS3;
        _fileServerSettings = fileServerSettingsOptions.Value;
    }

    public async Task<bool> MoveToPersistenceAsync(string fileKey)
    {
        var getResponse = await _amazonS3.GetObjectAsync(_fileServerSettings.TemporaryBucketName, fileKey);

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
            Key = fileKey,
            BucketName = _fileServerSettings.PersistenceBucketName,
            InputStream = getResponse.ResponseStream,
            ContentType = getResponse.Headers.ContentType
        };

        var putResponse = await _amazonS3.PutObjectAsync(putRequest);
        
        return putResponse.HttpStatusCode == HttpStatusCode.OK;
    }
}