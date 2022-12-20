using Ardalis.Specification;

namespace LiWiMus.Core.Artists.Specifications;

public sealed class ArtistsByUserIdSpec : Specification<Artist>
{
    public ArtistsByUserIdSpec(int userId)
    {
        Query.Where(artist => artist.UserArtists.Any(ua => ua.UserId == userId));
    }
}