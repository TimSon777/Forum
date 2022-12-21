using Application.Abstractions;
using Domain.Entities;
using Forum.Handler.Data;
using Microsoft.AspNetCore.SignalR;

namespace Forum.API;

public sealed class ChatConnector : IChatConnector
{
    private readonly IUserRepository _userRepository;
    private readonly IHubContext<SupportChat> _hub;

    private const string ConnectionUpMethod = "ConnectionUp";
    private const string ConnectionDownMethod = "ConnectionDown";

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

        var eventTask = _hub.Clients
            .Users(user.Name, mate.Name)
            .SendAsync(ConnectionUpMethod);
        
        await Task.WhenAll(commitTask, eventTask);
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
                .SendAsync(ConnectionDownMethod);

            user.Mate.Mate = null;
            
            var mate = user.Mate;

            var newUser = await _userRepository.FindActiveUserWithoutMateAsync(!isAdmin);

            if (newUser is not null)
            {
                await _hub.Clients
                    .Users(newUser.Name, mate.Name)
                    .SendAsync(ConnectionUpMethod);
                
                mate.Mate = newUser;
                newUser.Mate = mate;
            }
        }

        user.Mate = null;

        await _userRepository.CommitAsync();
    }

    public async Task SendMessageAsync(string userName, SendMessageHubItem message)
    {
        var user = await _userRepository.GetUserAsync(userName);
        
        await _hub.Clients
            .Users(user.Mate!.Name, user.Name)
            .SendAsync("ReceiveMessage", message);
    }
}