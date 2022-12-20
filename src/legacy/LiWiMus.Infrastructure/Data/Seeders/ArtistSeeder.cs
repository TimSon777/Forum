using LiWiMus.Core.Albums;
using LiWiMus.Core.Artists;
using LiWiMus.Core.Tracks;
using LiWiMus.Core.Users;
using LiWiMus.Core.Users.Enums;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LiWiMus.Infrastructure.Data.Seeders;

// ReSharper disable once UnusedType.Global
public class ArtistSeeder : ISeeder
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationContext _applicationContext;

    public ArtistSeeder(UserManager<User> userManager, ApplicationContext applicationContext)
    {
        _userManager = userManager;
        _applicationContext = applicationContext;
    }

    public async Task SeedAsync(EnvironmentType environmentType)
    {
        const string userName = "MockUser_Artist";
        
        if (await _userManager.FindByNameAsync(userName) is not null)
        {
            return;
        }
        
        switch (environmentType)
        {
            case EnvironmentType.Development:
                var user = new User
                {
                    Email = "mockEmail@mock.mock_Artist",
                    Gender = Gender.Male,
                    UserName = userName
                };

                var result = await _userManager.CreateAsync(user, "Password");

                if (!result.Succeeded)
                {
                    throw new SystemException();
                }
                
                var album = new Album
                {
                    Title = "MockAlbum_Artist",
                    CoverLocation = "Location",
                    PublishedAt = DateOnlyExtensions.Now
                };

                _applicationContext.Add(album);
                
                var track = new Track
                {
                    Duration = 120,
                    Name = "MockTrack_Artist",
                    FileLocation = "Location",
                    Album = album,
                    PublishedAt = DateOnlyExtensions.Now
                };

                _applicationContext.Add(track);

                var artist = new Artist
                {
                    About = "About",
                    Name = "MockArtist_Artist",
                    PhotoLocation = "Location",
                    UserArtists = new List<UserArtist>
                    {
                        new()
                        {
                            User = user
                        }
                    },
                    Albums = new List<Album> { album },
                    Tracks = new List<Track> { track }
                };

                _applicationContext.Add(artist);
                break;
            case EnvironmentType.Production:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
        }
    }

    public int Priority => 1;
}