using Chat.Infrastructure.Data;
using Chat.Infrastructure.Mapping;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.MessageHandlers;

public class MessageHub : Hub
{
    private readonly IBus _bus;
    private const string GroupName = "Forum";
    private const string ReceiveMessageMethod = "ReceiveMessage";
    
    public MessageHub(IBus bus)
    {
        _bus = bus;
    }

    public override Task OnConnectedAsync()
    {
        Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName);
        return base.OnDisconnectedAsync(exception);
    }

    // ReSharper disable once UnusedMember.Global
    public async Task SendMessageAsync(GetMessageItem messageItem)
    {
        var message = messageItem.ToMessage(); 
        
        var publishTask = _bus.Publish(message);
        
        var sendTask = Clients
            .Group(GroupName)
            .SendAsync(ReceiveMessageMethod, message);

        await Task.WhenAll(publishTask, sendTask);
    }
}