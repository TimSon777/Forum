using Ardalis.Specification;

namespace LiWiMus.Core.Users.Specifications;

public sealed class UserWithChatsByIdSpec : Specification<User>, ISingleResultSpecification
{
    public UserWithChatsByIdSpec(int id)
    {
        Query
            .Where(u => u.Id == id)
            .Include(u => u.UserChats)
            .ThenInclude(c => c.Messages)
            .ThenInclude(m => m.Owner);
    }
}