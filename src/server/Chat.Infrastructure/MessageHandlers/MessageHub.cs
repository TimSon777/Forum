using Chat.Infrastructure.Mapping;
using Chat.Infrastructure.MessageHandlers.Data;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.MessageHandlers;

public class MessageHub : Hub
{
    private readonly IBus _bus;
    private const string ReceiveMessageMethod = "ReceiveMessage";
    private readonly IValidator<GetMessageHubItem> _validator;

    public MessageHub(IBus bus, IValidator<GetMessageHubItem> validator)
    {
        _bus = bus;
        _validator = validator;
    }

    // ReSharper disable once UnusedMember.Global
    public async Task SendMessageAsync(GetMessageHubItem messageHubItem)
    {
        var validationResult = await _validator.ValidateAsync(messageHubItem);

        validationResult.EnsureSuccess();
        
        var message = messageHubItem.ToGetMessageConsumerItem();

        var publishTask = _bus.Publish(message);
        
        var sendTask = Clients.All
            .SendAsync(ReceiveMessageMethod, message);

        await Task.WhenAll(publishTask, sendTask);
    }
}