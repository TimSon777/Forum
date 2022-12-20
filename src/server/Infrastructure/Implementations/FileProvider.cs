using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Application.Abstractions;
using Domain.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Implementations;

public class FileProvider : IFileProvider
{
    private readonly IAmazonS3 _amazonS3;
    private readonly FileServerSettings _fileServerSettings;
    private readonly ILogger<FileProvider> _logger;

    public FileProvider(IAmazonS3 amazonS3, IOptions<FileServerSettings> fileServerSettingsOptions, ILogger<FileProvider> logger)
    {
        _amazonS3 = amazonS3;
        _logger = logger;
        _fileServerSettings = fileServerSettingsOptions.Value;
    }

    public async Task<Result<GetFileItem>> FindFileAsync(string key, CancellationToken token = new())
    {
        var response = await _amazonS3.GetObjectAsync(_fileServerSettings.PersistenceBucketName, key, token);

        if (!response.IsSuccess())
        {
            return Result<GetFileItem>.WithError($"File by key {key} was not found");
        }

        var result = new GetFileItem
        {
            File = response.ResponseStream,
            ContentType = response.Headers.ContentType
        };

        return Result.WithValue(result);
    }

    public async Task<Result<SavedFileItem>> SaveFileAsync(SaveFileItem file, CancellationToken token = new())
    {
        var fileKey = Guid.NewGuid();
        try
        {
            var request = new PutObjectRequest
            {
                BucketName = _fileServerSettings.TemporaryBucketName,
                Key = fileKey.ToString(),
                InputStream = file.File,
                ContentType = file.ContentType
            };

            var response = await _amazonS3.PutObjectAsync(request, token);

            if (!response.IsSuccess())
            {
                return Result<SavedFileItem>.WithError("File not saved.");
            }
            
            var result = new SavedFileItem
            {
                FileKey = fileKey
            };
                
            return Result.WithValue(result);

        }
        catch (AmazonS3Exception ex)
        {
            _logger.LogException(ex);
            return Result<SavedFileItem>.WithError("File not saved.");
        }
    }
}