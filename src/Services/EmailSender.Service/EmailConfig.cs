using DotNetEnv;
using EventBus.EventBus.Base;
using EventBus.EventBus.Base.Enums;

namespace EmailSender.Service;

public class EmailConfig

{
    public string Server { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Pass { get; set; }

    public static EmailConfig LoadFromEnv()
    {
        Env.Load("config.env");

        var config = new EmailConfig
        {
            Server = Environment.GetEnvironmentVariable("SMTP_HOST"),
            Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "0"),
            User = Environment.GetEnvironmentVariable("SMTP_USER"),
            Pass = Environment.GetEnvironmentVariable("SMTP_PASS"),
        }; 
        return config;
    }

    public static EventBusConfig RabbitMQConfig()
    {
        Env.Load("config.env");
        
      
        var config = new EventBusConfig
        {
            ConnectionRetryCount = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ__RETRYCOUNT") ?? "5"),
            EventBusType = EventBusType.RabbitMQ,
            DefaultTopicName = Environment.GetEnvironmentVariable("RABBITMQ__DEFAULTTOPIC"),
            SubscriberClientAppName = Environment.GetEnvironmentVariable("RABBITMQ__CLIENTAPPNAME"),
            EventNameSuffix = Environment.GetEnvironmentVariable("RABBITMQ__SUFFIX"),
            EventBusConnectionString = Environment.GetEnvironmentVariable("RABBITMQ__URI")
        };
        return config;
    }
}
