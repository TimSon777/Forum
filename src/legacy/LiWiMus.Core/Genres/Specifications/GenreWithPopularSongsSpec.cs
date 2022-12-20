using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Genres.Specifications;

public sealed class GenreWithPopularSongsSpec : Specification<Genre, Genre>, ISingleResultSpecification
{
    public GenreWithPopularSongsSpec(int id, int countSongs)
    {
        Query
            .Where(genre => genre.Id == id)
            .Include(genre => genre.Tracks
                .OrderByDescending(t => t.Owners.Count)
                .Take(countSongs))
            .ThenInclude(track => track.Album);
    }
}

public static partial class GenresRepositoryExtensions
{
    public static async Task<Genre?> GenreWithPopularSongsAsync(this IRepository<Genre> repository, int id, int countSongs = 20)
    {
        var spec = new GenreWithPopularSongsSpec(id, countSongs);
        return await repository.GetBySpecAsync(spec);
    }
}