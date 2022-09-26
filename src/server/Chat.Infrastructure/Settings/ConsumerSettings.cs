using Microsoft.Extensions.Configuration;

namespace Chat.Infrastructure.Settings;

// ReSharper disable once ClassNeverInstantiated.Global
public class ConsumerSettings
{
    public const string Position = "CONSUMER_SETTINGS";
    
    [ConfigurationKeyName("QUEUE_NAME")]
    public string QueueName { get; set; } = "";
    
    [ConfigurationKeyName("EXCHANGE_NAME")]
    public string ExchangeName { get; set; } = "";
    
    [ConfigurationKeyName("EXCHANGE_TYPE")]
    public string ExchangeType { get; set; } = "";
    
    [ConfigurationKeyName("ROUTING_KEY")]
    public string RoutingKey { get; set; } = "";
}