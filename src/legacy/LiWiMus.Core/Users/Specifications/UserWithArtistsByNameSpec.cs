using Ardalis.Specification;

namespace LiWiMus.Core.Users.Specifications;

public sealed class UserWithArtistsByNameSpec : Specification<User>, ISingleResultSpecification
{
    public UserWithArtistsByNameSpec(string userName)
    {
        Query.Where(user => user.UserName == userName)
             .Include(user => user.Artists);
    }
}