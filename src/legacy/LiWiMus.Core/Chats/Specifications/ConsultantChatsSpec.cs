using Ardalis.Specification;
using LiWiMus.Core.Chats.Enums;
using LiWiMus.Core.OnlineConsultants;

namespace LiWiMus.Core.Chats.Specifications;

public sealed class ConsultantChatsSpec : Specification<Chat>
{
    public ConsultantChatsSpec(OnlineConsultant consultant, ChatStatus status = ChatStatus.Opened)
    {
        Query.Where(chat => chat.ConsultantConnectionId == consultant.ConnectionId && chat.Status == status);
    }    
}