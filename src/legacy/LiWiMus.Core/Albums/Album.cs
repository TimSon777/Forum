using System.ComponentModel.DataAnnotations;
using LiWiMus.Core.Artists;
using LiWiMus.Core.Constants;
using LiWiMus.Core.LikedAlbums;
using LiWiMus.Core.Shared.Interfaces;
using LiWiMus.Core.Tracks;

namespace LiWiMus.Core.Albums;

public class Album : BaseEntity, IResource.WithMultipleOwners<Artist>
{
    [StringLength(50, MinimumLength = 5)]
    [RegularExpression(RegularExpressions.DisableTags)]
    public string Title { get; set; } = null!;

    public DateOnly PublishedAt { get; set; }
    public string CoverLocation { get; set; } = null!;

    public List<LikedAlbum> Subscribers { get; set; } = new();
    public List<Artist> Owners { get; set; } = new();
    public List<Track> Tracks { get; set; } = new();
}