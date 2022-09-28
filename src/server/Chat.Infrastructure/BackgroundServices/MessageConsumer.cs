using System.Text;
using System.Text.Json;
using Chat.DAL.Abstractions.Chat;
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
    private readonly ILogger<MessageConsumer> _logger;
    private readonly ConsumerSettings _consumerSettings;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly BrokerConnectionSettings _brokerConnectionSettings;

    public MessageConsumer(ILogger<MessageConsumer> logger, 
        IOptions<ConsumerSettings> consumerSettingsOptions, 
        IOptions<BrokerConnectionSettings> brokerConnectionSettings,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _consumerSettings = consumerSettingsOptions.Value;
        _brokerConnectionSettings = brokerConnectionSettings.Value;
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

    private Task OnShutdownAsync(object sender, ShutdownEventArgs @event)
    {
        _logger.LogWarning("Consumer was stopped");
        return Task.CompletedTask;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            Password = _brokerConnectionSettings.Password,
            HostName = _brokerConnectionSettings.Host,
            UserName = _brokerConnectionSettings.UserName,
            DispatchConsumersAsync = true
        };

        using var connection = factory.CreateConnection();
        
        using var channel = connection.CreateModel();
        
        channel.ExchangeDeclare(_consumerSettings.ExchangeName, _consumerSettings.ExchangeType, true);
        channel.QueueDeclare(_consumerSettings.QueueName, autoDelete: false);
        channel.QueueBind(_consumerSettings.QueueName, _consumerSettings.ExchangeName, _consumerSettings.RoutingKey);
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        
        consumer.Received += OnReceivedAsync;
        consumer.Shutdown += OnShutdownAsync;

        channel.BasicConsume(_consumerSettings.QueueName, true, consumer);

        try
        {
            await Task.Delay(-1, stoppingToken);
        }
        catch (TaskCanceledException)
        {
            consumer.Received -= OnReceivedAsync;
            consumer.Shutdown -= OnShutdownAsync;
        }
    }
}