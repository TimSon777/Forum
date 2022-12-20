using LiWiMus.Core.Chats.Enums;
using LiWiMus.Core.Messages;
using LiWiMus.Core.OnlineConsultants;
using LiWiMus.Core.OnlineConsultants.Specifications;
using Microsoft.AspNetCore.SignalR;

namespace LiWiMus.Web.MVC.Hubs.SupportChat;

public partial class SupportChatHub
{
    public async Task ConnectConsultant()
    {
        var user = await _userManager.GetUserAsync(Context.User);
        var oldCons = await _onlineConsultantsRepository.GetBySpecAsync(new ConsultantByUser(user));

        if (oldCons is not null)
        {
            return;
        }

        var consultant = new OnlineConsultant
        {
            Consultant = user,
            ConnectionId = Context.ConnectionId
        };

        await _onlineConsultantsRepository.AddAsync(consultant);
        await _onlineConsultantsRepository.SaveChangesAsync();
    }
    
    public async Task<Result> SendMessageToUser(string connectionId, string text)
    {
        var user = await _userManager.GetUserAsync(Context.User);

        var onlineConsultant = await _onlineConsultantsRepository
            .GetBySpecAsync(new OnlineConsultantByConnectionIdSpec(Context.ConnectionId));

        var chat = onlineConsultant?.Chats.FirstOrDefault(c => c.UserConnectionId == connectionId);

        if (chat is null)
        {
            return Result.Failure("Chat is not found.");
        }

        var message = await _messageRepository.AddAsync(new Message
        {
            Chat = chat,
            Text = text,
            Owner = user
        });

        await _messageRepository.SaveChangesAsync();

        await Clients
            .Group(chat.User.UserName)
            .SendAsync(GetMessageIdForUser, message.Id);
        
        return Result.Success(message.Id);
    }

    public async Task<Result> CloseChatByConsultant(string userName)
    {
        var user = await _userManager.GetUserAsync(Context.User);
        var consultant = await _onlineConsultantsRepository.GetBySpecAsync(new ConsultantByUser(user));
        var chat = consultant!.Chats.FirstOrDefault(c => c.User.UserName == userName && c.Status == ChatStatus.Opened);

        if (chat is null)
        {
            return Result.Failure($"Chat with {userName} is not found or is not opened.");
        }
        
        chat.Status = ChatStatus.ClosedByConsultant;

        await _chatRepository.SaveChangesAsync();

        await SendMessageToUser(chat.UserConnectionId, "Chat was closed by cons");
        return Result.Success();
    }
    
    public async Task DisconnectConsultant()
    {
        var user = await _userManager.GetUserAsync(Context.User);
        var consultant = await _onlineConsultantsRepository.GetBySpecAsync(new ConsultantByUser(user));

        if (consultant is null)
        {
            return;
        }

        var chats = consultant.Chats.Where(c => c.Status == ChatStatus.Opened);

        await _onlineConsultantsRepository.DeleteAsync(consultant);

        foreach (var chat in chats)
        {
            var newConsultant =
                await _onlineConsultantsRepository.GetBySpecAsync(new ConsultantWithMinimalWorkload(consultant));

            if (newConsultant is null)
            {
                await SendMessageToUser(chat.UserConnectionId,
                    "Your consultant leave chat, but we dont have available consultant for you");
                continue;
            }

            chat.ConsultantConnectionId = newConsultant.ConnectionId;
            newConsultant.Chats.Add(chat);
            
            await _chatRepository.SaveChangesAsync();
            await EstablishConnection(chat.User.UserName, newConsultant.ConnectionId, chat.UserConnectionId);
            await Clients.Group(chat.User.UserName).SendAsync(GetNewUserName, user.UserName);
            await SendMessageToUser(chat.UserConnectionId,
                "Your consultant leave chat, we found for you new consultant");
        }
    }
}