using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using File.Application.Abstractions;
using File.Domain.Data;
using FileMetadata.Queue.Shared;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace File.Infrastructure.Implementations;

public class FileProvider : IFileProvider
{
    private readonly IAmazonS3 _amazonS3;
    private readonly FileServerSettings _fileServerSettings;
    private readonly IBus _bus;

    public FileProvider(IAmazonS3 amazonS3, IOptions<FileServerSettings> fileServerSettingsOptions, IBus bus)
    {
        _amazonS3 = amazonS3;
        _bus = bus;
        _fileServerSettings = fileServerSettingsOptions.Value;
    }

    public async Task<Result<FileItem>> FindFileAsync(string key, CancellationToken token = new())
    {
        try
        {
            var response = await _amazonS3.GetObjectAsync(_fileServerSettings.PersistenceBucketName, key, token);

            if (response is null)
            {
                return Result<FileItem>.WithError($"File by key {key} was not found");
            }

            var result = new FileItem
            {
                Body = response.ResponseStream,
                Name = string.Join("-", response.BucketName, response.Key),
                ContentType = response.Headers.ContentType
            };

            return Result.WithValue(result);
        }
        catch (AmazonS3Exception ex)
        {
            return Result<FileItem>.WithError(ex.Message);
        }
    }

    public async Task SaveFileAsync(Guid requestId, IFormFile file, CancellationToken token = new())
    {
        try
        {
            var request = new PutObjectRequest
            {
                BucketName = _fileServerSettings.TemporaryBucketName,
                Key = requestId.ToString(),
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

            var response = await _amazonS3.PutObjectAsync(request, token);
            // TODO check response

            var contract = new FileAndMetadataContract
            {
                RequestId = requestId
            };

            await _bus.Publish(contract, token);
        }
        catch (AmazonS3Exception ex)
        {
            // TODO
        }
    }
}