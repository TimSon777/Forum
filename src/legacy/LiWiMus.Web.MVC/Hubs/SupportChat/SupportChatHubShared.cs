using LiWiMus.Core.Chats;
using LiWiMus.Core.Chats.Enums;
using LiWiMus.Core.Messages;
using LiWiMus.Core.OnlineConsultants;
using LiWiMus.Core.OnlineConsultants.Specifications;
using LiWiMus.Core.Users;
using LiWiMus.Core.Users.Specifications;
using LiWiMus.SharedKernel.Interfaces;
using LiWiMus.Web.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace LiWiMus.Web.MVC.Hubs.SupportChat;

public partial class SupportChatHub
{
    private readonly IRepository<Message> _messageRepository;
    private readonly IRepository<OnlineConsultant> _onlineConsultantsRepository;
    private readonly IRepository<Chat> _chatRepository;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<User> _userRepository;

    private const string GetNewUserName = nameof(GetNewUserName);
    private const string GetUserNameWhenLeft = nameof(GetUserNameWhenLeft);
    private const string GetUserNameWhenCloseByUser = nameof(GetUserNameWhenCloseByUser);
    private const string GetMessageIdForUser = nameof(GetMessageIdForUser);
    private const string GetMessageIdAndUserNameForConsultant = nameof(GetMessageIdAndUserNameForConsultant);
    
    public SupportChatHub(UserManager<User> userManager,
        IRepository<Chat> chatRepository, IRepository<OnlineConsultant> onlineConsultantsRepository,
        IRepository<Message> messageRepository,
        IRepository<User> userRepository)
    {
        _userManager = userManager;
        _chatRepository = chatRepository;
        _onlineConsultantsRepository = onlineConsultantsRepository;
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    private async Task<User> GetUserAsync()
    { 
        var spec = new UserWithChatsByIdSpec(Context.User!.GetId()!.Value); 
        return (await _userRepository.GetBySpecAsync(spec))!;
    }

    private async Task EstablishConnection(string groupName, 
        string consultantConnectionId,
        string userConnectionId)
    {
        await Groups.AddToGroupAsync(userConnectionId, groupName);
        await Groups.AddToGroupAsync(consultantConnectionId, groupName);
    }

    private async Task EstablishConnectionWhenConnectionsNotNull(string groupName, 
        string? consultantConnectionId,
        string? userConnectionId)
    {
        if (consultantConnectionId is not null && userConnectionId is not null)
        {
            await EstablishConnection(groupName, consultantConnectionId, userConnectionId);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var user = await GetUserAsync();
        var consultant = await _onlineConsultantsRepository.GetBySpecAsync(new ConsultantByUser(user));

        if (consultant is not null)
        {
            await DisconnectConsultant();
        }

        var chat = user.UserChats.FirstOrDefault(c => c.Status == ChatStatus.Opened);

        if (chat is not null)
        {
            await Clients
                .Group(user.UserName)
                .SendAsync(GetUserNameWhenLeft, user.UserName);
        }

        await base.OnDisconnectedAsync(exception);
    }
}