using Application.Abstractions;
using Domain.Entities;
using Forum.Handler.Data;
using Microsoft.AspNetCore.SignalR;

namespace Forum.API;

public sealed class ChatConnector : IChatConnector
{
    private readonly IUserRepository _userRepository;
    private readonly IHubContext<SupportChat> _hub;

    private const string ConnectionUpMessage = "ConnectionUp";
    private const string ConnectionDownMessage = "ConnectionDown";

    public ChatConnector(IUserRepository userRepository, IHubContext<SupportChat> hub)
    {
        _userRepository = userRepository;
        _hub = hub;
    }

    public async Task ConnectAsync(string userName, string connectionId, bool isAdmin)
    {
        var user = await _userRepository.FindUserAsync(userName, isAdmin);

        if (user is null)
        {
            user = new User
            {
                Name = userName,
                IsAdmin = isAdmin,
                Connections = new List<Connection>()
            };
            
            _userRepository.Add(user);
        }
        
        user.AddConnection(connectionId);

        await _userRepository.CommitAsync();

        if (user.Mate is not null)
        {
            return;
        }
        
        var mate = await _userRepository.FindActiveUserWithoutMateAsync(!isAdmin);

        if (mate is null)
        {
            return;
        }

        user.Mate = mate;
        mate.Mate = user;

        var commitTask = _userRepository.CommitAsync();

        var userTask = _hub.Clients
            .User(user.Name)
            .SendAsync(ConnectionUpMessage);
        
        var mateTask = _hub.Clients
            .User(mate.Name)
            .SendAsync(ConnectionUpMessage);

        await Task.WhenAll(commitTask, userTask, mateTask);
    }
    
    public async Task DisconnectAsync(string userName, string connectionId, bool isAdmin)
    {
        var user = await _userRepository.GetUserAsync(userName, isAdmin);
        
        user.RemoveConnection(connectionId);

        if (user.Connections.Any())
        {
            await _userRepository.CommitAsync();
            return;
        }
        
        if (user.Mate is not null)
        {
            await _hub.Clients
                .User(user.Mate.Name)
                .SendAsync(ConnectionDownMessage);

            user.Mate.Mate = null;
            
            var mate = user.Mate;

            var newUser = await _userRepository.FindActiveUserWithoutMateAsync(!isAdmin);

            if (newUser is not null)
            {
                await _hub.Clients
                    .User(mate.Name)
                    .SendAsync(ConnectionUpMessage);

                mate.Mate = newUser;
            }
        }

        user.Mate = null;

        await _userRepository.CommitAsync();
    }

    public async Task SendMessageAsync(string userName, SendMessageHubItem message)
    {
        var user = await _userRepository.GetUserAsync(userName);
        
        await _hub.Clients
            .User(user.Mate!.Name)
            .SendAsync("ReceiveMessage", message);
    }
}