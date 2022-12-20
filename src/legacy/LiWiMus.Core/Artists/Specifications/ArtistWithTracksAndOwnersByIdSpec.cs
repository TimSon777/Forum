using Ardalis.Specification;

namespace LiWiMus.Core.Artists.Specifications;

public sealed class ArtistWithTracksAndOwnersByIdSpec : Specification<Artist>, ISingleResultSpecification
{
    public ArtistWithTracksAndOwnersByIdSpec(int id)
    {
        Query.Where(artist => artist.Id == id)
             .Include(artist => artist.Tracks)
             .Include(artist => artist.Owners);
    }
}