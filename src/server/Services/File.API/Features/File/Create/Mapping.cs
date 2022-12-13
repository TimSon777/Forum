using Domain.Data;
using Domain.Events;

namespace File.API.Features.File.Create;

public static class Mapping
{
    public static SaveFileItem MapToSave(this Request request) => new()
    {
        File = request.File.OpenReadStream(),
        ContentType = request.File.ContentType
    };

    public static FileMetadataEvent MapToEvent(this Request request) => new()
    {
        RequestId = request.RequestId
    };
}