using System.ComponentModel.DataAnnotations;
using LiWiMus.Core.Albums;
using LiWiMus.Core.Constants;
using LiWiMus.Core.LikedArtists;
using LiWiMus.Core.Shared.Interfaces;
using LiWiMus.Core.Tracks;

namespace LiWiMus.Core.Artists;

public class Artist : BaseEntity, IResource.WithMultipleOwners<User>
{
    public List<UserArtist> UserArtists { get; set; } = new();
    public List<User> Owners { get; set; } = new();

    [StringLength(50, MinimumLength = 5)]
    [RegularExpression(RegularExpressions.DisableTags)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string About { get; set; } = null!;

    public string PhotoLocation { get; set; } = null!;

    public List<Track> Tracks { get; set; } = new();
    public List<LikedArtist> Subscribers { get; set; } = new();
    public List<Album> Albums { get; set; } = new();
}