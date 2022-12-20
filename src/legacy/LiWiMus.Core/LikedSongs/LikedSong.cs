using LiWiMus.Core.Tracks;

namespace LiWiMus.Core.LikedSongs;

public class LikedSong : BaseEntity
{
    public User User { get; set; } = null!;
    public Track Track { get; set; } = null!;
}