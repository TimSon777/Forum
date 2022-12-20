using Ardalis.Specification;

namespace LiWiMus.Core.OnlineConsultants.Specifications;

public sealed class ConsultantByUser : Specification<OnlineConsultant>, ISingleResultSpecification
{
    public ConsultantByUser(User user)
    {
        Query.Include(oc => oc.Chats)
            .ThenInclude(c => c.User)
            .Include(c => c.Chats)
            .ThenInclude(c => c.Messages)
            .Where(oc => oc.Consultant == user);
    }
}