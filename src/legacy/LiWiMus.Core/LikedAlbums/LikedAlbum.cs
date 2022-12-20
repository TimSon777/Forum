using LiWiMus.Core.Albums;

namespace LiWiMus.Core.LikedAlbums;

public class LikedAlbum : BaseEntity
{
    public User User { get; set; } = null!;
    public Album Album { get; set; } = null!;
}