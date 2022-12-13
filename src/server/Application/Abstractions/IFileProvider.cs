using Domain.Data;

namespace Application.Abstractions;

public interface IFileProvider
{
    Task<Result<GetFileItem>> FindFileAsync(string key, CancellationToken token = new());
    Task<Result<SavedFileItem>> SaveFileAsync(SaveFileItem file, CancellationToken token = new());
}