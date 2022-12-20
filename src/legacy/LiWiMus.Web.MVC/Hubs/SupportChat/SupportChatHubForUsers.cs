using System.ComponentModel.DataAnnotations;
using LiWiMus.Core.Chats;
using LiWiMus.Core.Chats.Enums;
using LiWiMus.Core.Chats.Specifications;
using LiWiMus.Core.Messages;
using LiWiMus.Core.OnlineConsultants;
using LiWiMus.Core.OnlineConsultants.Specifications;
using LiWiMus.Core.Users;
using LiWiMus.SharedKernel.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace LiWiMus.Web.MVC.Hubs.SupportChat;

public partial class SupportChatHub : Hub
{
    public async Task<Result> CloseChatByUser()
    {
        var user = await GetUserAsync();
        var chat = user.UserChats.FirstOrDefault(c => c.Status == ChatStatus.Opened);

        if (chat is null)
        {
            return Result.Failure("Chat is not found");
        }

        chat.Status = ChatStatus.ClosedByUser;
        await _chatRepository.SaveChangesAsync();
        
        await Clients
            .Groups(user.UserName)
            .SendAsync(GetUserNameWhenCloseByUser, user.UserName);
        
        return Result.Success();
    }
    
    private async Task ConnectUserWithExistChat(User user, Chat chat)
    {
        var consultant = chat.ConsultantConnectionId is null
            ? null
            : await _onlineConsultantsRepository
                .GetBySpecAsync(new OnlineConsultantByConnectionIdSpec(chat.ConsultantConnectionId));

        if (consultant is null)
        {
            consultant = await _onlineConsultantsRepository.GetBySpecAsync(new ConsultantWithMinimalWorkload());
            consultant?.Chats.Add(chat);
        }

        chat.UserConnectionId = Context.ConnectionId;
        await _chatRepository.SaveChangesAsync();
        await EstablishConnectionWhenConnectionsNotNull(user.UserName, 
            consultant?.ConnectionId, Context.ConnectionId);
    }

    private async Task ConnectUserWithoutExistChat(User user)
    {
        var consultant = await _onlineConsultantsRepository.GetBySpecAsync(new ConsultantWithMinimalWorkload());

        var chat = new Chat
        {
            User = user,
            UserConnectionId = Context.ConnectionId,
            ConsultantConnectionId = consultant?.ConnectionId
        };

        if (consultant is null)
        {
            await _chatRepository.AddAsync(chat);
        }
        else
        {
            consultant.Chats.Add(chat);
        }

        await _chatRepository.SaveChangesAsync();

        await EstablishConnectionWhenConnectionsNotNull(user.UserName, 
            consultant?.ConnectionId, Context.ConnectionId);
    }

    public async Task ConnectUser()
    {
        var user = await GetUserAsync();
        var chatOld = user.UserChats.FirstOrDefault(c => c.Status == ChatStatus.Opened);

        await (chatOld is null
            ? ConnectUserWithoutExistChat(user)
            : ConnectUserWithExistChat(user, chatOld));

        await Clients
            .Group(user.UserName)
            .SendAsync(GetNewUserName, user.UserName);
    }

    public async Task<Result> SendMessageToConsultant([StringLength(100)] string text)
    {
        var user = await _userManager.GetUserAsync(Context.User);
        var chat = await _chatRepository.GetBySpecAsync(new OpenedChatByUserSpec(user));

        if (chat is null)
        {
            return Result.Failure("Chat is not found");
        }

        if (text.IsNullOrEmpty())
        {
            return Result.Failure();
        }

        var message = await _messageRepository.AddAsync(new Message
        {
            Text = text,
            Owner = user,
            Chat = chat
        });
        
        await _messageRepository.SaveChangesAsync();

        if (chat.ConsultantConnectionId is not null)
        {
            await Clients
                .Group(user.UserName)
                .SendAsync(GetMessageIdAndUserNameForConsultant, message.Id, user.UserName);
        }

        return Result.Success(message.Id);
    }
}