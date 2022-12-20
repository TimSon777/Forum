using System.ComponentModel.DataAnnotations;
using LiWiMus.Core.Albums;
using LiWiMus.Core.Artists;
using LiWiMus.Core.Constants;
using LiWiMus.Core.Genres;
using LiWiMus.Core.LikedSongs;
using LiWiMus.Core.Playlists;
using LiWiMus.Core.PlaylistTracks;
using LiWiMus.Core.Shared.Interfaces;

namespace LiWiMus.Core.Tracks;

public class Track : BaseEntity, IResource.WithMultipleOwners<Artist>
{
    public List<Artist> Owners { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();

    public Album Album { get; set; } = null!;
    
    [StringLength(50, MinimumLength = 5)]
    [RegularExpression(RegularExpressions.DisableTags)]
    public string Name { get; set; } = null!;

    public DateOnly PublishedAt { get; set; }
    public string FileLocation { get; set; } = null!;
    public double Duration { get; set; }

    public List<LikedSong> Subscribers { get; set; } = new();
    public List<Playlist> Playlists { get; set; } = new();
    public List<PlaylistTrack> PlaylistTracks { get; set; } = new();
}