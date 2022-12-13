using Domain.Events;
using Forum.Handler;
using Infrastructure.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Forum.API;

public class NotificationFileUploadedConsumer : IConsumer<FileUploadedEvent>
{
    private readonly IHubContext<MessageHub> _hubContext;
    private readonly ICachingService _cachingService;

    public NotificationFileUploadedConsumer(IHubContext<MessageHub> hubContext, ICachingService cachingService)
    {
        _hubContext = hubContext;
        _cachingService = cachingService;
    }

    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        var connectionId = await _cachingService.FindConnectionIdAsync(context.Message.RequestId);

        if (connectionId is null)
        {
            return;
        }

        await _hubContext.Clients
            .Client(connectionId)
            .SendAsync("ReceiveFileUploadedNotification");
    }
}