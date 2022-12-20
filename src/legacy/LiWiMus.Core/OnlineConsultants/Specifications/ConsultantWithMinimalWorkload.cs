using Ardalis.Specification;
using LiWiMus.Core.Chats.Enums;

namespace LiWiMus.Core.OnlineConsultants.Specifications;

public sealed class ConsultantWithMinimalWorkload : Specification<OnlineConsultant>, ISingleResultSpecification
{
    public ConsultantWithMinimalWorkload(OnlineConsultant? exclude = null)
    {
        if (exclude is not null)
        {
            Query.Where(oc => oc != exclude);
        }
        
        Query
            .Include(ch => ch.Chats)
            .OrderBy(oc => oc.Chats.Count(ch => ch.Status == ChatStatus.Opened));
    }
}