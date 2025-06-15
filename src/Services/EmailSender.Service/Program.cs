using System.Globalization;
using EmailSender.Service;
using EmailSender.Service.Events;
using EmailSender.Service.Events.SendOtp;
using EmailSender.Service.Events.SendVerificationLink;
using EventBus.EventBus.Base.Abstraction;
using EventBus.Factory;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("<<EmailSender service started>>");
ServiceCollection services = new ServiceCollection();

services.AddSingleton<SendOtpVerificationEmailIntegrationEventHandler>();
services.AddSingleton<SendVerificationLinkIntegrationEventHandler>();
services.AddSingleton<IEmailSender, EmailSender.Service.EmailService>();
services.AddSingleton<IEventBus>(provider =>
{
    return EventBusFactory.Create(
        EmailConfig.RabbitMQConfig(), provider);
});

IServiceProvider serviceProvider = services.BuildServiceProvider();
IEventBus eventBus = serviceProvider.GetRequiredService<IEventBus>();

await eventBus.Subscribe<SendOtpVerificationEmailIntegrationEvent, SendOtpVerificationEmailIntegrationEventHandler>();
await eventBus.Subscribe<SendVerificationLinkIntegrationEvent, SendVerificationLinkIntegrationEventHandler>();

Console.Read();