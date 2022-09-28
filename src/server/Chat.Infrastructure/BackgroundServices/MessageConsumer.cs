using System.Text;
using System.Text.Json;
using Chat.DAL.Abstractions.Chat;
using Chat.Infrastructure.Channels;
using Chat.Infrastructure.Mapping;
using Chat.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Exception = Chat.Core.Exception;
using GetMessageItem = Chat.Infrastructure.Data.GetMessageItem;

namespace Chat.Infrastructure.BackgroundServices;

public class MessageConsumer : BackgroundService
{
    private readonly IModel _chanel;
    private readonly ILogger<MessageConsumer> _logger;
    private readonly ConsumerSettings _consumerSettings;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public MessageConsumer(IMessageProcessorChannel messageProcessorChannel, 
        ILogger<MessageConsumer> logger, 
        IOptions<ConsumerSettings> consumerSettingsOptions, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _chanel = messageProcessorChannel.Channel;
        _consumerSettings = consumerSettingsOptions.Value;
    }

    private async Task OnReceivedAsync(object _, BasicDeliverEventArgs args)
    {
        try
        {
            var message = Encoding.UTF8
                .GetFromJson<GetMessageItem>(args.Body)
                .ToMessage();

            using var scope = _serviceScopeFactory.CreateScope();
            
            var addMessageItem = message.ToAddMessageItem();
            var isOk = await scope.ServiceProvider
                .GetRequiredService<IChatRepository>()
                .AddMessageAsync(addMessageItem);
            
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

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _chanel.ExchangeDeclare(_consumerSettings.ExchangeName, _consumerSettings.ExchangeType, true);
        _chanel.QueueDeclare(_consumerSettings.QueueName, autoDelete: false);
        _chanel.QueueBind(_consumerSettings.QueueName, _consumerSettings.ExchangeName, _consumerSettings.RoutingKey);
        
        var consumer = new AsyncEventingBasicConsumer(_chanel);
        
        consumer.Received += OnReceivedAsync;
        _chanel.BasicConsume(_consumerSettings.QueueName, true, consumer);
        
        return Task.CompletedTask;
    }
}