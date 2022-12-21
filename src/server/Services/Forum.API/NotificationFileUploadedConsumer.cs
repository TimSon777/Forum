using Domain.Events;
using Infrastructure.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Forum.API;

public class NotificationFileUploadedConsumer : IConsumer<FileUploadedEvent>
{
    private readonly IHubContext<SupportChat> _hubContext;
    private readonly ICachingService _cachingService;

    public NotificationFileUploadedConsumer(IHubContext<SupportChat> hubContext, ICachingService cachingService)
    {
        _hubContext = hubContext;
        _cachingService = cachingService;
    }

    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        var requestId = context.Message.RequestId;
        var connectionIdTask = _cachingService.FindConnectionIdAsync(context.Message.RequestId);
        var fileKeyTask = _cachingService.FindFileIdAsync(requestId);

        await Task.WhenAll(connectionIdTask, fileKeyTask);

        var connectionId = await connectionIdTask;
        var fileKey = await fileKeyTask;
        
        if (connectionId is null || fileKey is null)
        {
            return;
        }

        await _hubContext.Clients
            .Client(connectionId)
            .SendAsync("ReceiveFileUploadedNotification", fileKey);
    }
}