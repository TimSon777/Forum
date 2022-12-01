using FluentValidation;
using Forum.Infrastructure.Mapping;
using Forum.Infrastructure.MessageHandlers.Data;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Forum.Infrastructure.MessageHandlers;

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

        var consumerMessage = messageHubItem.Map();
        var sendMessage = consumerMessage.Map();
        
        var publishTask = _bus.Publish(consumerMessage);
        
        var sendTask = Clients.All
            .SendAsync(ReceiveMessageMethod, sendMessage);

        await Task.WhenAll(publishTask, sendTask);
    }
}