using Ardalis.Specification;

namespace LiWiMus.Core.Artists.Specifications;

public sealed class ArtistByNameSpecification : Specification<Artist>, ISingleResultSpecification
{
    public ArtistByNameSpecification(string name)
    {
        Query.Where(artist => artist.Name == name);
    }
}