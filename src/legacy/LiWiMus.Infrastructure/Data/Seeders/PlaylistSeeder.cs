using LiWiMus.Core.Albums;
using LiWiMus.Core.Artists;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.PlaylistTracks;
using LiWiMus.Core.Settings;
using LiWiMus.Core.Tracks;
using LiWiMus.Core.Users;
using LiWiMus.Core.Users.Enums;
using LiWiMus.SharedKernel.Extensions;
using LiWiMus.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LiWiMus.Infrastructure.Data.Seeders;

// ReSharper disable once UnusedType.Global
public class PlaylistSeeder : ISeeder
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationContext _applicationContext;
    private readonly AdminSettings _adminSettings;

    public PlaylistSeeder(UserManager<User> userManager,
        IOptions<AdminSettings> adminSettingsOptions,
        ApplicationContext applicationContext)
    {
        _userManager = userManager;
        _applicationContext = applicationContext;
        _adminSettings = adminSettingsOptions.Value;
    }
    public async Task SeedAsync(EnvironmentType environmentType)
    {
        const string userName = "MockUser_Playlist";
        
        if (await _userManager.FindByNameAsync(userName) is not null)
        {
            return;
        }
        
        switch (environmentType)
        {
            case EnvironmentType.Development:
                var artist = new Artist
                {
                    About = "About",
                    Name = "MockArtist_Playlist",
                    PhotoLocation = "Location"
                };

                _applicationContext.Add(artist);

                var album = new Album
                {
                    Title = "MockAlbum_Playlist",
                    CoverLocation = "Location",
                    Owners = new List<Artist> {artist},
                    PublishedAt = DateOnlyExtensions.Now
                };

                _applicationContext.Add(album);

                var track1 = new Track
                {
                    Id = 24,
                    Album = album,
                    Name = "MockTrack_Playlist",
                    Duration = 190,
                    FileLocation = "Location",
                    PublishedAt = DateOnlyExtensions.Now
                };
                
                var track2 = new Track
                {
                    Id = 100,
                    Album = album,
                    Name = "MockTrack_Playlist",
                    Duration = 190,
                    FileLocation = "Location",
                    PublishedAt = DateOnlyExtensions.Now
                };
                
                var track3 = new Track
                {
                    Id = 101,
                    Album = album,
                    Name = "MockTrack_Playlist",
                    Duration = 190,
                    FileLocation = "Location",
                    PublishedAt = DateOnlyExtensions.Now
                };

                _applicationContext.Add(track1);
                _applicationContext.Add(track2);
                _applicationContext.Add(track3);

                var user = new User
                {
                    UserName = userName,
                    Email = "mockEmail@mock.mock_Playlist",
                    Gender = Gender.Female,
                };

                var result = await _userManager.CreateAsync(user, "Password");

                if (!result.Succeeded)
                {
                    throw new SystemException();
                }
                
                var playlist = new Playlist
                {
                    Id = 24,
                    Name = "MockPlaylist_Playlist",
                    Owner = user,
                    PhotoLocation = "Location"
                };

                _applicationContext.Add(playlist);

                var playlistTrack = new PlaylistTrack
                {
                    Playlist = playlist,
                    Track = track1
                };

                var additionPlaylist = new List<Playlist>
                {
                    new()
                    {
                        Name = "MockPlaylist_Playlist",
                        Owner = user,
                        PhotoLocation = "Location"
                    },

                    new()
                    {
                        Name = "MockPlaylist_Playlist",
                        Owner = user,
                        PhotoLocation = "Location"
                    },

                    new()
                    {
                        Name = "MockPlaylist_Playlist",
                        Owner = user,
                        PhotoLocation = "Location"
                    },

                    new()
                    {
                        Name = "MockPlaylist_Playlist",
                        Owner = user,
                        PhotoLocation = "Location"
                    }
                };

                foreach (var p in additionPlaylist)
                {
                    _applicationContext.Add(p);
                }

                var admin = await _userManager.FindByNameAsync(_adminSettings.UserName);
                
                var playlistAdmin = new Playlist
                {
                    Name = "MockPlaylist_Playlist",
                    Owner = admin,
                    PhotoLocation = "Location"
                };
                
                _applicationContext.Add(playlistAdmin);

                _applicationContext.Add(playlistTrack);
                break;
            case EnvironmentType.Production:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
        }
    }

    public int Priority => 20;
}