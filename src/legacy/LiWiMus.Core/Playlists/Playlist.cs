using System.ComponentModel.DataAnnotations;
using LiWiMus.Core.Constants;
using LiWiMus.Core.LikedPlaylists;
using LiWiMus.Core.PlaylistTracks;
using LiWiMus.Core.Shared.Interfaces;
using LiWiMus.Core.Tracks;

namespace LiWiMus.Core.Playlists;

public class Playlist : BaseEntity, IResource.WithOwner<User>
{
    public User Owner { get; set; } = null!;
    [StringLength(50, MinimumLength = 5)]
    [RegularExpression(RegularExpressions.DisableTags)]
    public string Name { get; set; } = null!;

    public bool IsPublic { get; set; }
    public string? PhotoLocation { get; set; }

    public List<Track> Tracks { get; set; } = new();
    public List<PlaylistTrack> PlaylistTracks { get; set; } = new();
    public List<LikedPlaylist> Subscribers { get; set; } = new();
}