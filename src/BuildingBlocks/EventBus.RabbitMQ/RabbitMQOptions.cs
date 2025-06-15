using EventBus.EventBus.Base;
using EventBus.EventBus.Base.Enums;
using Microsoft.Extensions.Configuration;

namespace EventBus.RabbitMQ;

public sealed class RabbitMQOptions
{
    public static EventBusConfig GetConfigRabbitMQ(string configPath)
    {
        var config = LoadConfiguration(configPath);
        return new EventBusConfig()
        {
            ConnectionRetryCount = int.Parse(config["RabbitMQ:RetryCount"]),
            EventNameSuffix = config["RabbitMQ:Suffix"] ?? "IntegrationEvent",
            SubscriberClientAppName = config["RabbitMQ:ClientAppName"] ?? "default-client-name",
            DefaultTopicName = config["RabbitMQ:DefaultTopic"] ?? "default-topic-name",
            EventBusType = EventBusType.RabbitMQ,
            EventBusConnectionString = config["RabbitMQ:Uri"] ?? throw new Exception("RabbitMQ URI is missing in appsettings.json")
        };
    }

    private static IConfigurationRoot LoadConfiguration(string basePath)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        return builder.Build();
    }
}