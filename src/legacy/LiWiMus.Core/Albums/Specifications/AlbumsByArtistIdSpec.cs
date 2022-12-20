using Ardalis.Specification;

namespace LiWiMus.Core.Albums.Specifications;

public sealed class AlbumsByArtistIdSpec : Specification<Album>
{
    public AlbumsByArtistIdSpec(int artistId)
    {
        Query.Where(album => album.Owners.Any(a => a.Id == artistId));
    }
}