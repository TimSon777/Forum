using System.Text;
using System.Text.Json;
using Chat.Infrastructure.Chanels;
using Chat.Infrastructure.Data;
using Chat.Infrastructure.Mapping;
using Chat.Infrastructure.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Chat.Infrastructure.BackgroundServices;

public class MessageConsumer : BackgroundService
{
    private readonly IModel _chanel;
    private readonly ILogger<MessageConsumer> _logger;
    private readonly ConsumerSettings _consumerSettings;

    public MessageConsumer(IMessageProcessorChanel messageProcessorChanel, 
        ILogger<MessageConsumer> logger, 
        IOptions<ConsumerSettings> consumerSettingsOptions)
    {
        _logger = logger;
        _chanel = messageProcessorChanel.Channel;
        _consumerSettings = consumerSettingsOptions.Value;
    }

    private async Task OnReceivedAsync(object _, BasicDeliverEventArgs args)
    {
        try
        {
            var messageItem = Encoding.UTF8.GetFromJson<GetMessageItem>(args.Body);
            var message = messageItem.ToMessage();
            _logger.LogInformation("Message: {message}", message);
        }
        catch (JsonException)
        {
            _logger.LogWarning("Wrong json format");
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