#region

using LiWiMus.Core.Settings;
using LiWiMus.Web.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

#endregion

namespace LiWiMus.Web.Shared.Services;

public class FormFileSaver : IFormFileSaver
{
    private readonly SharedSettings _settings;

    public FormFileSaver(IOptions<SharedSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<string> SaveWithRandomNameAsync(IFormFile file, DataType type)
    {
        var fakeDirectory = type switch
        {
            DataType.Music => _settings.DataSettings.MusicDirectory,
            DataType.Picture => _settings.DataSettings.PicturesDirectory,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        var realDirectory = Path.Combine(_settings.SharedDirectory, fakeDirectory);

        var fileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(file.FileName));
        var realPath = Path.Combine(realDirectory, fileName);
        var fakePath = Path.Combine(fakeDirectory, fileName);

        await SaveAsync(file, realPath);
        return fakePath;
    }

    private static async Task SaveAsync(IFormFile file, string path)
    {
        var fileInfo = new FileInfo(path);
        await using var stream = fileInfo.OpenWrite();
        await file.CopyToAsync(stream);
    }
}