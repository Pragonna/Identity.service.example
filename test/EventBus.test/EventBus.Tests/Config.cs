using EventBus.EventBus.Base;
using EventBus.EventBus.Base.Enums;
using Microsoft.Extensions.Configuration;

namespace EventBus.IntegrationTest;

public class Config
{
    public static EventBusConfig GetConfigRabbitMQ
    {
        get
        {
            // var config = LoadConfiguration();
            return new EventBusConfig()
            {
                ConnectionRetryCount = 5,
                EventNameSuffix = "IntegrationEvent",
                SubscriberClientAppName = "default-client-name",
                DefaultTopicName = "default-topic-name",
                EventBusType = EventBusType.RabbitMQ,
                EventBusConnectionString = "connectionString"
            };
        }
    }

    // private static IConfigurationRoot LoadConfiguration()
    // {
    //     var builder = new ConfigurationBuilder()
    //         .SetBasePath(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../../test/EventBus.test/")))
    //         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    //
    //     return builder.Build();
    // }
}