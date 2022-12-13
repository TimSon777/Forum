﻿using FluentValidation;
using Forum.Handler;
using Forum.Handler.Data;
using Infrastructure.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Forum.API;

public class MessageHub : Hub
{
    private readonly IBus _bus;
    private readonly IValidator<GetMessageHubItem> _validator;
    private readonly ICachingService _cachingService;

    public MessageHub(IBus bus, IValidator<GetMessageHubItem> validator, ICachingService cachingService)
    {
        _bus = bus;
        _validator = validator;
        _cachingService = cachingService;
    }

    // ReSharper disable once UnusedMember.Global
    public async Task SendMessage(GetMessageHubItem messageHubItem)
    {
        await _validator.ValidateAndThrowAsync(messageHubItem);

        var consumerMessage = messageHubItem.Map();
        var sendMessage = consumerMessage.Map();
        
        var publishTask = _bus.Publish(consumerMessage);
        
        var sendTask = Clients.All
            .SendAsync("ReceiveMessage", sendMessage);

        await Task.WhenAll(publishTask, sendTask);
    }

    // ReSharper disable once UnusedMember.Global
    public async Task SaveConnectionId(Guid requestId)
    {
        await _cachingService.SaveConnectionIdAsync(requestId, Context.ConnectionId);
    }
}