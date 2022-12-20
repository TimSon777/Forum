using Ardalis.Specification;
using LiWiMus.Core.Chats.Enums;

namespace LiWiMus.Core.Chats.Specifications;

public sealed class OpenChatSpec : Specification<Chat>, ISingleResultSpecification
{
    public OpenChatSpec(User user)
    {
        Query.Where(chat => chat.User == user && chat.Status == ChatStatus.Opened);
    }
}