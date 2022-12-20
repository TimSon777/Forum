#region

using LiWiMus.Core.Interfaces;
using LiWiMus.Core.Interfaces.Files;
using LiWiMus.Core.Users;
using Refit;

#endregion

namespace LiWiMus.Infrastructure.Services;

public class AvatarService : IAvatarService
{
    private const string ApiUriFormat = "https://avatars.dicebear.com/api/adventurer/{0}.svg?background=%23EF6817";
    private readonly IFileService _fileService;

    public AvatarService(IFileService fileService)
    {
        _fileService = fileService;
    }

    private static string GetRandomSeed()
    {
        return Random.Shared.Next().ToString();
    }

    public async Task SetRandomAvatarAsync(User user)
    {
        try
        {
            var request = new SaveUrlRequest {Url = string.Format(ApiUriFormat, GetRandomSeed())};
            var fileResult = await _fileService.Save(request);
            if (!fileResult.IsSuccessStatusCode || fileResult.Content is null)
            {
                return;
            }

            if (user.AvatarLocation is not null)
            {
                await _fileService.Remove(user.AvatarLocation[1..]);
            }

            user.AvatarLocation = fileResult.Content.Location;
        }
        catch (ApiException)
        {
            user.AvatarLocation = null;
        }
    }
}