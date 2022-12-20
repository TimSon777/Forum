namespace LiWiMus.Core.Artists;

public class UserArtist : BaseEntity
{
    public User User { get; set; } = null!;
    public Artist Artist { get; set; } = null!;

    public int UserId { get; set; }
    public int ArtistId { get; set; }
}