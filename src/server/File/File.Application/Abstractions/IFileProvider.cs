using File.Domain.Data;
using Microsoft.AspNetCore.Http;

namespace File.Application.Abstractions;

public interface IFileProvider
{
    Task<Result<FileItem>> FindFileAsync(string key, CancellationToken token = new());
    Task SaveFileAsync(Guid requestId, IFormFile file, CancellationToken token = new());
}