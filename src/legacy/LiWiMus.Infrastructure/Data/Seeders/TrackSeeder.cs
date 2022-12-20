using LiWiMus.Core.Albums;
using LiWiMus.Core.Artists;
using LiWiMus.Core.Genres;
using LiWiMus.Core.Tracks;
using LiWiMus.Core.Users;
using LiWiMus.Core.Users.Enums;
using LiWiMus.SharedKernel.Extensions;
using Microsoft.AspNetCore.Identity;

namespace LiWiMus.Infrastructure.Data.Seeders;

// ReSharper disable once UnusedType.Global
public class TrackSeeder : ISeeder
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationContext _applicationContext;

    public TrackSeeder(UserManager<User> userManager, ApplicationContext applicationContext)
    {
        _userManager = userManager;
        _applicationContext = applicationContext;
    }

    public async Task SeedAsync(EnvironmentType environmentType)
    {
        const string userName = "MockUser_Track";

        if (await _userManager.FindByNameAsync(userName) is not null)
        {
            return;
        }

        switch (environmentType)
        {
            case EnvironmentType.Development:
                var user = new User
                {
                    Email = "mockEmail@mail.ru_Track",
                    Gender = Gender.Male,
                    Id = 220000,
                    UserName = userName
                };

                var result = await _userManager.CreateAsync(user, "Password");
                
                if (!result.Succeeded)
                {
                    throw new SystemException();
                }

                var artistOwner = new Artist
                {
                    Id = 220000,
                    About = "About",
                    Name = "MockArtistOwner_Track",
                    PhotoLocation = "Location",
                    Owners = new List<User> {user}
                };

                var artistNotOwner = new Artist
                {
                    Id = 220001,
                    About = "About",
                    Name = "MockArtistNotOwner_Track",
                    PhotoLocation = "Location",
                    Owners = new List<User> {user}
                };

                _applicationContext.Add(artistNotOwner);

                var album = new Album
                {
                    Owners = new List<Artist> {artistOwner},
                    Id = 220000,
                    CoverLocation = "Location",
                    Title = "MockAlbum_Track",
                    PublishedAt = DateOnlyExtensions.Now
                };

                var genreToTrack = new Genre
                {
                    Id = 220000,
                    Name = "MockGenreToTrack_Track"
                };

                var genreNotToTrack = new Genre
                {
                    Id = 220001,
                    Name = "MockGenreNotToTrack_Track"
                };

                _applicationContext.Add(genreNotToTrack);

                var track = new Track
                {
                    Owners = new List<Artist> {artistOwner},
                    Album = album,
                    Genres = new List<Genre> {genreToTrack},
                    Id = 220000,
                    Name = "MockTrack_Track",
                    FileLocation = "Location",
                    PublishedAt = DateOnlyExtensions.Now
                };

                _applicationContext.Add(track);
                break;
            case EnvironmentType.Production:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, null);
        }
    }

    public int Priority => 23;
}