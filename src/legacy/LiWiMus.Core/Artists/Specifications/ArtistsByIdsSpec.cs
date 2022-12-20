using Ardalis.Specification;

namespace LiWiMus.Core.Artists.Specifications;

public sealed class ArtistsByIdsSpec : Specification<Artist>
{
    public ArtistsByIdsSpec(int[] ids)
    {
        Query.Where(artist => ids.Contains(artist.Id));
    }
}