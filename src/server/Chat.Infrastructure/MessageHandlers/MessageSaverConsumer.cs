using System.Text.Json;
using Chat.Core.Data;
using Chat.DAL.Abstractions.Chat;
using Chat.Infrastructure.Mapping;
using MassTransit;
using Microsoft.Extensions.Logging;
using Exception = Chat.Core.Exception;

namespace Chat.Infrastructure.MessageHandlers;

// ReSharper disable once UnusedType.Global
public class MessageSaverConsumer : IConsumer<Message>
{
    private readonly IChatRepository _chatRepository;
    private readonly ILogger<MessageSaverConsumer> _logger;

    public MessageSaverConsumer(IChatRepository chatRepository, ILogger<MessageSaverConsumer> logger)
    {
        _chatRepository = chatRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<Message> context)
    {
        var message = context.Message;
        var addMessageItem = message.ToAddMessageItem();

        try
        {
            var isOk = await _chatRepository.AddMessageAsync(addMessageItem);
            if (!isOk)
            {
                _logger.LogInformation("Message was not saved in the database: {message}", message);
            }
        }
        catch (JsonException)
        {
            _logger.LogWarning("Wrong json format");
        }
        catch (Exception.ValidationException)
        {
            _logger.LogWarning("Validation exception");
        }
    }
}