using Ardalis.Specification;

namespace LiWiMus.Core.Messages.Specifications;

public sealed class MessageWithSenderByIdSpec : Specification<Message>, ISingleResultSpecification
{
    public MessageWithSenderByIdSpec(int id)
    {
        Query
            .Where(m => m.Id == id)
            .Include(m => m.Owner);
    }
}