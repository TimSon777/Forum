using Ardalis.Specification;

namespace LiWiMus.Core.Users.Specifications;

public sealed class UserByIdSpec : Specification<User>, ISingleResultSpecification
{
    public UserByIdSpec(int id)
    {
        Query.Where(user => user.Id == id);
    }
}