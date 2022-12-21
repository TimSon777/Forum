using System.Security.Claims;
using FluentValidation;
using Forum.API.Data;
using Infrastructure.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Forum.API;

public class SupportChat : Hub
{
    private readonly IBus _bus;
    private readonly IValidator<GetMessageHubItem> _validator;
    private readonly ICachingService _cachingService;
    private readonly IChatConnector _chatConnector;

    public SupportChat(IBus bus, IValidator<GetMessageHubItem> validator, ICachingService cachingService, IChatConnector chatConnector)
    {
        _bus = bus;
        _validator = validator;
        _cachingService = cachingService;
        _chatConnector = chatConnector;
    }

    public override async Task OnConnectedAsync()
    {
        await _chatConnector.ConnectAsync(Context.User!.UserName(), Context.ConnectionId, Context.User!.IsAdmin());
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _chatConnector.DisconnectAsync(Context.User!.UserName(), Context.ConnectionId, Context.User!.IsAdmin());
    }

    // ReSharper disable once UnusedMember.Global
    public async Task SendMessage(GetMessageHubItem messageHubItem)
    {
        await _validator.ValidateAndThrowAsync(messageHubItem);

        var consumerMessage = messageHubItem.Map(Context.User!.UserName());
        var sendMessage = consumerMessage.Map();
        
        var publishTask = _bus.Publish(consumerMessage);

        var sendTask = _chatConnector.SendMessageAsync(Context.User!.UserName(), sendMessage);

        await Task.WhenAll(publishTask, sendTask);
    }

    // ReSharper disable once UnusedMember.Global
    public async Task SaveConnectionId(Guid requestId)
    {
        await _cachingService.SaveConnectionIdAsync(requestId, Context.ConnectionId);
    }
}