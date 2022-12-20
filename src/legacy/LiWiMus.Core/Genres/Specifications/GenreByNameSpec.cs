using Ardalis.Specification;

namespace LiWiMus.Core.Genres.Specifications;

public sealed class GenreByNameSpec : Specification<Genre>, ISingleResultSpecification
{
    public GenreByNameSpec(string name)
    {
        Query.Where(genre => genre.Name == name);
    }
}