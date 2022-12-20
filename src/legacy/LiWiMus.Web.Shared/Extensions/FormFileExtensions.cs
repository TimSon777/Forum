using Microsoft.AspNetCore.Http;
using Refit;

namespace LiWiMus.Web.Shared.Extensions;

public static class FormFileExtensions
{
    public static StreamPart ToStreamPart(this IFormFile file)
    {
        return new StreamPart(file.OpenReadStream(), file.FileName, file.ContentType);
    }
}