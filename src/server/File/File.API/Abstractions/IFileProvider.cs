using File.API.Data;

namespace File.API.Abstractions;

public interface IFileProvider
{
    Task<Result<GetFileItem>> FindFileAsync(string key, CancellationToken token = new());
    Task<Result<GetSavedFileInfoItem>> SaveFileAsync(Stream file, CancellationToken token = new());
}