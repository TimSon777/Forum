using Refit;

namespace LiWiMus.Core.Interfaces.Files;

public interface IFileService
{
    [Multipart]
    [Post("/files")]
    Task<IApiResponse<FileLocation>> Save(StreamPart file);

    [Post("/files/url")]
    Task<IApiResponse<FileLocation>> Save(SaveUrlRequest request);

    [Delete("/{**path}")]
    Task<IApiResponse> Remove(string path);
}