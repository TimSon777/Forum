using Ardalis.Specification;

namespace LiWiMus.Core.Genres.Specifications;

public sealed class GenresByIdsSpec : Specification<Genre>
{
    public GenresByIdsSpec(int[] ids)
    {
        Query.Where(genre => ids.Contains(genre.Id));
    }
}