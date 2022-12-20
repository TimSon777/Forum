using LiWiMus.Core.Artists;

namespace LiWiMus.Core.LikedArtists;

public class LikedArtist : BaseEntity
{
    public User User { get; set; } = null!;
    public Artist Artist { get; set; } = null!;
}