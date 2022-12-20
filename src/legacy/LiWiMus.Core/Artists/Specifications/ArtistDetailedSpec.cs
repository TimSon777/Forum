using Ardalis.Specification;
using LiWiMus.SharedKernel.Interfaces;

namespace LiWiMus.Core.Artists.Specifications;

public sealed class ArtistDetailedSpec : Specification<Artist>, ISingleResultSpecification
{
    public ArtistDetailedSpec(int id)
    {
        Query.Where(artist => artist.Id == id)
             .Include(artist => artist.Albums)
             .Include(artist => artist.Owners);
    }
}

public static partial class ArtistsRepositoryExtensions
{
    public static async Task<Artist?> WithAlbumsAndOwnersAsync(this IRepository<Artist> repository, int id)
    {
        var spec = new ArtistDetailedSpec(id);
        return await repository.GetBySpecAsync(spec);
    }
}