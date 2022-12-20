using Ardalis.Specification;

namespace LiWiMus.Core.Artists.Specifications;

public sealed class ArtistWithOwnersByIdSpec : Specification<Artist>, ISingleResultSpecification
{
    public ArtistWithOwnersByIdSpec(int id)
    {
        Query.Where(artist => artist.Id == id)
             .Include(artist => artist.Owners)
             .Include(artist => artist.Albums);
    }
}