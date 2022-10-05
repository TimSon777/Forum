using System.Text.Json;
using Chat.DAL.Abstractions.Chat;
using Chat.Infrastructure.Mapping;
using Chat.Infrastructure.MessageHandlers.Data;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Chat.Infrastructure.MessageHandlers;

// ReSharper disable once UnusedType.Global
// ReSharper disable once ClassNeverInstantiated.Global
public class MessageSaverConsumer : IConsumer<GetMessageConsumerItem>
{
    private readonly IChatRepository _chatRepository;
    private readonly ILogger<MessageSaverConsumer> _logger;

    public MessageSaverConsumer(IChatRepository chatRepository, ILogger<MessageSaverConsumer> logger)
    {
        _chatRepository = chatRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GetMessageConsumerItem> context)
    {
        var message = context
            .Message
            .ToAddMessageStorageItem();

        try
        {
            var isOk = await _chatRepository.AddMessageAsync(message);
            if (!isOk)
            {
                _logger.LogInformation("Message was not saved in the database: {message}", message);
            }
        }
        catch (JsonException)
        {
            _logger.LogWarning("Wrong json format");
        }
        catch (ValidationException)
        {
            _logger.LogWarning("Validation exception");
        }
    }
}