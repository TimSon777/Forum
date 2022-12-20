using LiWiMus.Core.Playlists;

namespace LiWiMus.Core.LikedPlaylists;

public class LikedPlaylist : BaseEntity
{
    public User User { get; set; } = null!;
    public Playlist Playlist { get; set; } = null!;
}