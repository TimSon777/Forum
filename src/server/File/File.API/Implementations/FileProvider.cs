using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using File.API.Abstractions;
using File.API.Configurations;
using File.API.Data;
using Microsoft.Extensions.Options;

namespace File.API.Implementations;

public class FileProvider : IFileProvider
{
    private readonly IAmazonS3 _amazonS3;
    private readonly FileServerSettings _fileServerSettings;

    public FileProvider(IAmazonS3 amazonS3, IOptions<FileServerSettings> fileServerSettingsOptions)
    {
        _amazonS3 = amazonS3;
        _fileServerSettings = fileServerSettingsOptions.Value;
    }

    public async Task<Result<GetFileItem>> FindFileAsync(string key, CancellationToken token = new())
    {
        try
        {
            var response = await _amazonS3.GetObjectAsync(_fileServerSettings.BucketName, key, token);

            if (response is null)
            {
                return Result<GetFileItem>.WithError($"File by key {key} was not found");
            }

            var result = new GetFileItem
            {
                Body = response.ResponseStream,
                Name = string.Join("-", response.BucketName, response.Key),
                ContentType = response.Headers.ContentType
            };

            return Result.WithValue(result);
        }
        catch (AmazonS3Exception ex)
        {
            return Result<GetFileItem>.WithError(ex.Message);
        }
    }

    public async Task<Result<GetSavedFileInfoItem>> SaveFileAsync(IFormFile file, CancellationToken token = new())
    {
        try
        {
            var key = Guid
                .NewGuid()
                .ToString();

            var request = new PutObjectRequest
            {
                BucketName = _fileServerSettings.BucketName,
                Key = key,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType
            };

            var response = await _amazonS3.PutObjectAsync(request, token);

            if (!response.IsSuccess())
            {
                return Result<GetSavedFileInfoItem>.WithError($"Request to S3 storage was not success. Status code {response.HttpStatusCode}");
            }

            var result = new GetSavedFileInfoItem
            {
                Key = key,
            };
            
            return Result.WithValue(result);
        }
        catch (AmazonS3Exception ex)
        {
            return Result<GetSavedFileInfoItem>.WithError(ex.Message);
        }
    }
}